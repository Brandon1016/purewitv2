using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class DifusionController : Controller
    {
        IListas list;
        ITipo_Difusiones tipDif;
        IMensajes mens;
        IDifusiones dif;
        ITelefonos tel;
        IContactos_Por_Lista conList;
        IContactos con;
        IHistoriales his;
        public string link = "http://";
        

        IUsuarios use;
        ILista_Negra listNegra;

        public DifusionController()
        {
            list    = new MListas();
            tipDif  = new MTipo_Difusiones();
            mens    = new MMensajes();
            dif     = new MDifusiones();
            tel     = new MTelefonos();
            conList = new MContactos_Por_Lista();
            con     = new MContactos();
            his     = new MHistoriales();
            use     = new MUsuarios();
            listNegra = new MLista_Negra();
            
        }
        public ActionResult Index()
        {
            
            try
            {
                if (Session["Username"] != null)
                {
                    /*********************************CREAR*ENVIOS***********************************************/
                    List<DATA.Listas> listas = list.ListarListas();
                    List<DATA.Tipo_Difusiones> tipoDifusiones = tipDif.ListarTipoDifusiones();
                    List<DATA.Mensajes> mensajes = mens.ListarMensajes();
                    var listasDelUsuario = Mapper.Map<List<Models.Listas>>(listas.Where(x => x.Username == Session["Username"].ToString()));
                    var listaDeTipos = Mapper.Map<List<Models.Tipo_Difusiones>>(tipoDifusiones);
                    var listaMensajes = Mapper.Map<List<Models.Mensajes>>(mensajes.Where(x => x.Username == Session["Username"].ToString()));

                    IEnumerable<SelectListItem> selectListas =
                    from l in listasDelUsuario
                    select new SelectListItem
                    {
                        Text = l.Nombre,
                        Value = l.Id_Lista.ToString()
                    };

                    ViewBag.Listas = selectListas;

                    IEnumerable<SelectListItem> selectTipoDifusion =
                    from t in listaDeTipos
                    select new SelectListItem
                    {
                        Text = t.Descripcion,
                        Value = t.Id.ToString()
                    };

                    ViewBag.ListasTipoDifusion = selectTipoDifusion;

                    IEnumerable<SelectListItem> selectMensajes =
                    from m in listaMensajes
                    select new SelectListItem
                    {
                        Text = m.Cuerpo_Mensaje,
                        Value = m.Id_Mensaje.ToString()
                    };

                    ViewBag.ListaMensajes = selectMensajes;

                    /*********************************MOSTRAR*ENVIOS***********************************************/

                    var listaDeDifusiones = dif.ListarDifusines().Where(x => x.Username == Session["Username"].ToString());
                    List<Models.Difusiones> difusiones = Mapper.Map<List<Models.Difusiones>>(listaDeDifusiones);
                    foreach (Models.Difusiones dfs in difusiones)
                    {
                        var varas = tipDif.ListarTipoDifusiones().Where(x => x.Id == dfs.Id_Tipo_Mensaje).FirstOrDefault();
                        dfs.Des_tipo_Mensaje = varas.Descripcion;
                    }


                    ViewBag.ListaDeDifusiones = difusiones;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Rol = Session["Rol"].ToString();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Rol = Session["Rol"].ToString();
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Index(Models.Difusiones difusion)
        {
            try
            {
                string url = Request.UrlReferrer.Authority;
                link = "http://" + url;

                if (ModelState.IsValid)
                {
                    bool permitirEnvio = false;
                    DATA.Telefonos telefonoDelUsuario = tel.ListarTelefonos().Where(x => x.Username == Session["Username"].ToString()).FirstOrDefault();
                    if (telefonoDelUsuario == null)
                    {
                        ModelState.AddModelError("error", "Usted no tiene un número para enviar mensajes.");
                        return Index();
                    }
                    
                    DateTime dateSend;
                    var dateNow = DateTime.Now;
                    difusion.Fecha = dateNow;
                    TimeSpan ts;
                    ts = dateNow - dateNow;

                    string tipoEnvio = Request.Form["tipoEnvio"];
                    difusion.Username = Session["Username"].ToString();

                    //Debug.WriteLine(tipoEnvio);
                    if (difusion.Id_Tipo_Mensaje == 3 && difusion.passwordCorreo == null)
                    {
                        ModelState.AddModelError("errorPassword", "Debe escribir la contraseña de su correo para enviar el mensaje.");
                    }

                        if (tipoEnvio == "inmediato")
                    {
                        ts = dateNow - dateNow;
                        difusion.Fecha_Activacion = dateNow;
                        permitirEnvio = true;
                    }
                    else
                    {
                        try
                        {
                            string fechaPro = Request.Form["fechaPro"];
                            string[] datos = fechaPro.Split('-');
                            string horaPro = Request.Form["horaPro"];
                            string[] datos2 = horaPro.Split(':');
                            dateSend = new DateTime(Int32.Parse(datos[0]), Int32.Parse(datos[1]), Int32.Parse(datos[2]), Int32.Parse(datos2[0]), Int32.Parse(datos2[1]), 00);

                            if (dateSend > dateNow)
                            {
                                ts = dateSend - dateNow;
                                permitirEnvio = true;
                            } else
                            {
                                ModelState.AddModelError("fechaerror", "No puede programar un envio para una fecha ya pasada.");
                            }

                            difusion.Fecha_Activacion = dateSend;
                        } catch (Exception e)
                        {
                            ModelState.AddModelError("fechaerror", "Revise la fecha, ya que algo no parece estar bien.");
                        }
                    }

                    if (permitirEnvio)
                    {
                        var difusionInsertar = Mapper.Map<DATA.Difusiones>(difusion);
                        dif.InsertarDifusiones(difusionInsertar);

                        int maxId = dif.ListarDifusines().Where(x => x.Username == Session["Username"].ToString()).Max(x => x.Id_Difusion);
                        
                        DATA.Mensajes mensaje = mens.ListarMensajes().Where(x => x.Id_Mensaje == difusion.Id_Mensaje).First();
                        DATA.Difusiones difusion2 = dif.BuscarDifusiones(maxId);
                        DATA.Usuarios correoDelUsuario = use.ListarUsuarios().Where(x => x.Username == Session["Username"].ToString()).First();

                        Task.Delay(ts).ContinueWith((x) => enviarMensajes(telefonoDelUsuario.Numero, telefonoDelUsuario.Account_Id, telefonoDelUsuario.Authtoken, mensaje.Cuerpo_Mensaje, difusion.Id_Lista, difusion2, correoDelUsuario.Correo, difusion.passwordCorreo));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Index();
                    }
                }
                else
                {
                    return Index();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido registrar la difusion.");
                return Index();
            }
            
        }

            
        public void enviarMensajes(string UserPhone, string accountSid, string authToken, string mensaje, int idLista, DATA.Difusiones difusion, string correoUsuario ,string passwordCorreo)
        {
            try
            {
                difusion.Id_Estado = 2;
                dif.ActualizarDifusiones(difusion);

                List<DATA.Lista_Negra> listaNegra = listNegra.ListarListaNegra();
                var lista_Negra = Mapper.Map<List<Models.Lista_Negra>>(listaNegra);

                TwilioClient.Init(accountSid, authToken);

                var contactos = conList.Listar().Where(x => x.Id_Lista == idLista);

                foreach (DATA.Contactos_Por_Listas infoContacto in contactos)
                {

                    var noEnviar = lista_Negra.Where(x => x.Id_Contacto == infoContacto.Id_contacto).FirstOrDefault();
                    if (noEnviar == null)
                    {
                        Console.WriteLine("no esta en lista naegra: " + infoContacto.Id_contacto);
                        DATA.Contactos contacto = con.BuscarContactos(infoContacto.Id_contacto);
                        DATA.Historiales historial = new DATA.Historiales();
                        historial.Id_Difusion = difusion.Id_Difusion;
                        historial.Id_Contacto = contacto.Id_Contacto;

                        if (difusion.Id_Tipo_Mensaje == 1)
                        {
                            var message = MessageResource.Create(
                                body: mensaje,
                                from: new Twilio.Types.PhoneNumber("+" + UserPhone),
                                statusCallback: new Uri(link + "/MessageStatus"),
                                to: new Twilio.Types.PhoneNumber("+" + contacto.Numero)
                            );

                            historial.Id_Message = message.Sid;
                            historial.Estado = 6;

                        }
                        else
                        {
                            if (difusion.Id_Tipo_Mensaje == 2)
                            {
                                Debug.WriteLine("Voz");
                                Guid id = Guid.NewGuid();
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><Say voice=\"alice\" language=\"es-ES\">" + mensaje + "</Say></Response>");
                                String rootPath = Server.MapPath("~");
                                rootPath = rootPath + "\\Content\\xml\\" + id + ".xml";
                                doc.Save(rootPath);

                                var call = CallResource.Create(
                                    method: Twilio.Http.HttpMethod.Get,
                                    url: new Uri(link + "/Content/xml/" + id + ".xml"),
                                    from: new Twilio.Types.PhoneNumber("+" + UserPhone),
                                    to: new Twilio.Types.PhoneNumber("+" + contacto.Numero),
                                    statusCallback: new Uri(link + "/LlamadasStatus")
                                );

                                historial.Id_Message = call.Sid;
                                historial.Estado = 6;
                            }
                            else
                            {
                                if (difusion.Id_Tipo_Mensaje == 3)
                                {
                                    Debug.WriteLine("Correo");
                                    MailMessage email = new MailMessage();
                                    email.To.Add(new MailAddress(contacto.Correo));
                                    email.From = new MailAddress("xxx@gmail.com");
                                    email.Subject = "Asunto ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
                                    email.Body = mensaje;
                                    email.IsBodyHtml = true;
                                    email.Priority = MailPriority.Normal;

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "Smtp.Gmail.com";
                                    smtp.Port = 587;
                                    smtp.EnableSsl = true;
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new NetworkCredential(correoUsuario, passwordCorreo);

                                    //string output = null;
                                    historial.Id_Message = "--";

                                    try
                                    {
                                        smtp.Send(email);
                                        email.Dispose();
                                        //output = "Corre electrónico fue enviado satisfactoriamente.";
                                        historial.Estado = 4;
                                    }
                                    catch (Exception ex)
                                    {
                                        //output = "Error enviando correo electrónico: " + ex.Message;
                                        historial.Estado = 7;
                                    }

                                }
                            }
                        }

                        his.InsertarHistoriales(historial);
                    }
                    else
                    {
                        Console.WriteLine("esta en lista naegra: " + infoContacto.Id_contacto);
                    }


                }

                difusion.Id_Estado = 3;
                dif.ActualizarDifusiones(difusion);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
        }
    }
}