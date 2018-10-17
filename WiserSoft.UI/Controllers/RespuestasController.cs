using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class RespuestasController : Controller
    {
        IRespuestas Rest;
        IContactos cont;
        IComunicaciones com;
        ITelefonos tel;
        // GET: Respuestas
        public RespuestasController()
        {
            Rest = new MRespuestas();
            cont = new MContactos();
            com  = new MComunicaciones();
            tel  = new MTelefonos();
        }

        public ActionResult Index()
        {
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                ViewBag.userId = Session["Username"];
                DATA.Telefonos telefono = new DATA.Telefonos();
                telefono = tel.ListarTelefonos().Where(x => x.Username == Session["Username"].ToString()).FirstOrDefault();
                var personasConComunicacion = com.ListarComunicaciones().Where(x => x.Numero_Twilio == telefono.Numero).Select(x => x.Id_Contacto).Distinct();
                var lista = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString()).Where(x => personasConComunicacion.Contains(x.Id_Contacto));
                var contactos = Mapper.Map<List<Models.Contactos>>(lista);

                foreach (Models.Contactos a in contactos)
                {
                    var per = com.ListarComunicaciones().Where(x => x.Numero_Twilio == telefono.Numero).Where(x => x.Id_Contacto == a.Id_Contacto).Where(x => x.Estado == 6).FirstOrDefault();
                    if (per != null)
                    {
                        a.lista_negra = "Si";
                    }
                    else
                    {
                        a.lista_negra = "No";
                    }

                }
                
                return View(contactos);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "No tiene un número asignado para recibir respuestas.";
                return View();
            }
            
        }

        public ActionResult Mensajes(int Id_Contacto)
        {
            ViewBag.Contacto = Id_Contacto;
            ViewBag.prueba = Session["Username"];
            ViewBag.Rol = Session["Rol"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Mensajes(Models.Comunicaciones comunicaciones)
        {
            ViewBag.Rol = Session["Rol"].ToString();

            try
            {
                DATA.Telefonos telefono = new DATA.Telefonos();
                telefono = tel.ListarTelefonos().Where(x => x.Username == Session["Username"].ToString()).FirstOrDefault();
                var dateNow = DateTime.Now;
                comunicaciones.Fecha = dateNow;
                comunicaciones.Estado = 4;
                comunicaciones.Numero_Twilio = telefono.Numero;
                DATA.Contactos contacto = new DATA.Contactos();
                contacto = cont.BuscarContactos(comunicaciones.Id_Contacto);

                TwilioClient.Init(telefono.Account_Id, telefono.Authtoken);
                var message = MessageResource.Create(
                    body: comunicaciones.Mensaje,
                    from: new Twilio.Types.PhoneNumber("+" + telefono.Numero),
                    to: new Twilio.Types.PhoneNumber("+" + contacto.Numero)
                );

                com.InsertarComunicaciones(Mapper.Map<DATA.Comunicaciones>(comunicaciones));

               
                return RedirectToAction("Mensajes", new { Id_Contacto = comunicaciones.Id_Contacto });
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("Mensajes");
            }
            
        }

        [HttpGet]
        public JsonResult ListarMensajes(int Id_Contacto)
        {
            var lista = Rest.ListarComunicaciones().Where(x => x.Id_Contacto == Id_Contacto);
            var listas = Mapper.Map<List<Models.Respuestas>>(lista);

            var comuni = com.ListarComunicaciones().Where(x => x.Id_Contacto == Id_Contacto).Where(x => x.Estado ==6);
            
            foreach (DATA.Comunicaciones con in comuni)
            {
                con.Estado = 5;
                com.ActualizarComunicaciones(con);
            }

            return Json(listas, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarRecibidos()
        {
        
                ViewBag.userId = Session["Username"];
                DATA.Telefonos telefono = new DATA.Telefonos();
                telefono = tel.ListarTelefonos().Where(x => x.Username == Session["Username"].ToString()).FirstOrDefault();
                var personasConComunicacion = com.ListarComunicaciones().Where(x => x.Numero_Twilio == telefono.Numero).Where(x => x.Estado == 6).Select(x => x.Id_Contacto).Distinct();
                var lista = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString()).Where(x => personasConComunicacion.Contains(x.Id_Contacto));
                var contactos = Mapper.Map<List<Models.Contactos>>(lista);

                foreach (Models.Contactos a in contactos)
                {
                    var max = com.ListarComunicaciones().Where(x => x.Numero_Twilio == telefono.Numero).Where(x => x.Id_Contacto == a.Id_Contacto).Select(x => x.Fecha).Max();
                    var per = com.ListarComunicaciones().Where(x => x.Numero_Twilio == telefono.Numero).Where(x => x.Id_Contacto == a.Id_Contacto).Where(x => x.Fecha == max).FirstOrDefault();

                    a.Detalle = per.Mensaje;
                    a.lista_negra = per.Fecha.ToString();
               
                }
            return Json(contactos, JsonRequestBehavior.AllowGet);
        }
    }
}