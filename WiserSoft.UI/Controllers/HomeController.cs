using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;

namespace WiserSoft.UI.Controllers
{
    public class dataChart
    {
        public int value;
        public string name;

        public dataChart(int _value, string _name)
        {
            value = _value;
            name = _name;
        }
    }
    public class HomeController : Controller
    {
        IUsuarios usu;
        IContactos cont;
        IDifusiones difu;
        IEstados est;
        IHistoriales his;
        IReporteclientesactivos rep;

        public HomeController()
        {
            usu = new MUsuarios();
            cont = new MContactos();
            difu = new MDifusiones();
            est = new MEstados();
            his = new MHistoriales();
            rep = new MReporteclientesactivos();
        }
        public ActionResult Index()
        {
            ViewBag.Mensaje = "no";
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Usuarios usuario)
        {
            try
            {
                var passwordEncripted = Encriptacion.Encriptacion.Encriptar(usuario.Password);
                var loginResultUsers = usu.BuscarUsuarios(usuario.Username, passwordEncripted);
                Console.WriteLine(passwordEncripted);
                if (loginResultUsers)
                { //Si es nulo no existe
                    DATA.Usuarios datos = usu.BuscarUsuarios(usuario.Username);
                    Session["Username"] = datos.Username;
                    Session["Rol"] = datos.Id_rol;

                    return RedirectToAction("UserDashboard", "Home");

                }
                else
                {
                    ViewBag.Mensaje = "si";
                    ModelState.AddModelError("errorLogin", "Usuario y/o contraseña incorrectos.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                ModelState.AddModelError("errorLogin", "Usuario y/o contraseña incorrectos.");
                return View("Index");
            }
            
        }

        public ActionResult UserDashboard()
        {
            int contactos;
            int sms;
            int llamadas;
            int correos;
            ViewBag.Rol = Session["Rol"].ToString();
            if (Session["Rol"].ToString() == "2")
            {

                contactos = cont.ListarContactos().Count();
                ViewBag.totalContactos = contactos;

                sms = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 1).Count();
                ViewBag.totalSMS = sms;

                llamadas = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 2).Count();
                ViewBag.totalLlamadas = llamadas;

                correos = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 3).Count();
                ViewBag.totalLCorreos = correos;

                var listaDifusiones = difu.CantidadDifusiones();
                Console.WriteLine(listaDifusiones);
                var dif = Mapper.Map<List<Models.Difusiones>>(listaDifusiones);


                foreach (Models.Difusiones a in dif)
                {
                    Console.WriteLine("Estado:" + a.Descripcion + " Cantidad:" + a.Id_Estado);

                }


                /*Grafico Pie Difusiones */
                int cantidadDifu = difu.ListarDifusines().Count();
                if (cantidadDifu > 0)
                {
                    List<DATA.Difusiones> listadifusiones = Mapper.Map<List<DATA.Difusiones>>(difu.ListarDifusines());
                    var difusiones = listadifusiones.Select(x => x.Id_Estado).Distinct();

                    List<dataChart> listaDifusion = new List<dataChart>();
                    foreach (var item in difusiones)
                    {
                        listaDifusion.Add(new dataChart(Int32.Parse(listadifusiones.Count(x => x.Id_Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data = listaDifusion.ToList();
                }
                else
                {
                    ViewBag.cantidadDifu = 0;
                    ViewBag.group = null;
                    ViewBag.data = null;
                }
                /*Fin Grafico Pie Difusiones */

                /*Sacando el estado de los mensajes de la última difusión de tipo texto (1)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 1).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group1 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data1 = listaHistoriales.ToList();
                    ViewBag.nombre1 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group1 = null;
                    ViewBag.data1 = null;
                    ViewBag.nombre1 = null;
                }
                /*Termina el de tipo texto (1)*/
                /*Sacando el estado de los mensajes de la última difusión de tipo voz (2)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 2).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group2 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data2 = listaHistoriales.ToList();
                    ViewBag.nombre2 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group2 = null;
                    ViewBag.data2 = null;
                    ViewBag.nombre2 = null;
                }
                /*Termina el de tipo voz (2)*/
                /*Sacando el estado de los mensajes de la última difusión de tipo correo (3)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 3).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group3 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data3 = listaHistoriales.ToList();
                    ViewBag.nombre3 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group3 = null;
                    ViewBag.data3 = null;
                    ViewBag.nombre3 = null;
                }
                /*Termina el de tipo correo (3)*/

                /*Grafico 4 - Reporte Clientes Activos*/
                try
                {

                    int cantidadActivos = rep.ListarReporteclientesactivos().Count();

                    var lista = rep.ListarReporteclientesactivos();
                    var reporte = Mapper.Map<List<Models.Reporteclientesactivos>>(lista);


                    foreach (Models.Reporteclientesactivos a in reporte)
                    {
                        Console.WriteLine("Cliente:" + a.Nombre + " Cantidad:" + a.Cantidad);

                    }

                    List<DATA.Reporteclientesactivos> listareportes = rep.ListarReporteclientesactivos();
                    var reporteslistados = listareportes.Select(x => x.Id_Contacto).Distinct();

                    List<dataChart> listaReporte = new List<dataChart>();
                    foreach (var item in reporteslistados)
                    {
                        listaReporte.Add(new dataChart(rep.ListarReporteclientesactivos().Where(x => x.Id_Contacto == item).Select(x => x.Cantidad).First(),
                            cont.ListarContactos().Where(x => x.Id_Contacto == item).Select(x => x.Nombre).First().ToString()));
                    }
                    ViewBag.group4 = cont.ListarContactos().Select(x => x.Nombre).Distinct();
                    ViewBag.data4 = listaReporte.ToList();
                    ViewBag.nombre4 = cantidadActivos;
                }
                catch (Exception e)
                {
                    ViewBag.group4 = null;
                    ViewBag.data4 = null;
                    ViewBag.nombre4 = 0;
                }
                /*Fin de Grafico 4*/

                /*Table Contactos*/
                try
                {
                    var lista2 = cont.ListarContactos();
                    var contactos2 = Mapper.Map<List<Models.Contactos>>(lista2);

                    return View(contactos2);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "No se ha podido mostrar la tabla");
                }

            }
            else
            if (Session["Rol"].ToString() == "1")
            {
                contactos = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString()).Count();
                ViewBag.totalContactos = contactos;

                sms = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 1).Count();
                ViewBag.totalSMS = sms;

                llamadas = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 2).Count();
                ViewBag.totalLlamadas = llamadas;

                correos = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 3).Count();
                ViewBag.totalLCorreos = correos;

                var listaDifusiones = difu.CantidadDifusiones();
                Console.WriteLine(listaDifusiones);
                var dif = Mapper.Map<List<Models.Difusiones>>(listaDifusiones);


                foreach (Models.Difusiones a in dif)
                {
                    Console.WriteLine("Estado:" + a.Descripcion + " Cantidad:" + a.Id_Estado);

                }
                /*Grafico Pie Difusiones */
                int cantidadDifu = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString()).Count();
                if (cantidadDifu > 0)
                {
                    List<DATA.Difusiones> listadifusiones = Mapper.Map<List<DATA.Difusiones>>(difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString()));
                    var difusiones = listadifusiones.Select(x => x.Id_Estado).Distinct();

                    List<dataChart> listaDifusion = new List<dataChart>();
                    foreach (var item in difusiones)
                    {
                        listaDifusion.Add(new dataChart(Int32.Parse(listadifusiones.Count(x => x.Id_Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data = listaDifusion.ToList();
                }
                else
                {
                    ViewBag.cantidadDifu = 0;
                    ViewBag.group = null;
                    ViewBag.data = null;
                }
                /*Fin Grafico Pie Difusiones */

                /*Sacando el estado de los mensajes de la última difusión de tipo texto (1)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 1).Where(x => x.Username == Session["Username"].ToString()).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group1 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data1 = listaHistoriales.ToList();
                    ViewBag.nombre1 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group1 = null;
                    ViewBag.data1 = null;
                    ViewBag.nombre1 = null;
                }
                /*Termina el de tipo texto (1)*/
                /*Sacando el estado de los mensajes de la última difusión de tipo voz (2)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 2).Where(x => x.Username == Session["Username"].ToString()).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group2 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data2 = listaHistoriales.ToList();
                    ViewBag.nombre2 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group2 = null;
                    ViewBag.data2 = null;
                    ViewBag.nombre2 = null;
                }
                /*Termina el de tipo voz (2)*/


                /*Sacando el estado de los mensajes de la última difusión de tipo correo (3)*/
                try
                {
                    int maximaDifusion = difu.ListarDifusines().Where(x => x.Id_Tipo_Mensaje == 3).Where(x => x.Username == Session["Username"].ToString()).Select(x => x.Id_Difusion).Max();
                    string nombreDifusion = difu.BuscarDifusiones(maximaDifusion).Descripcion;
                    List<DATA.Historiales> listahistoriales = Mapper.Map<List<DATA.Historiales>>(his.ListarHistoriales().Where(x => x.Id_Difusion == maximaDifusion));
                    var historiales = listahistoriales.Select(x => x.Estado).Distinct();

                    List<dataChart> listaHistoriales = new List<dataChart>();
                    foreach (var item in historiales)
                    {
                        listaHistoriales.Add(new dataChart(Int32.Parse(listahistoriales.Count(x => x.Estado == item).ToString()), est.ListarEstados().Where(x => x.Id == item).Select(x => x.Descripcion).First().ToString()));
                    }

                    ViewBag.group3 = est.ListarEstados().Select(x => x.Descripcion).Distinct();
                    ViewBag.data3 = listaHistoriales.ToList();
                    ViewBag.nombre3 = nombreDifusion;
                }
                catch (Exception e)
                {
                    ViewBag.group3 = null;
                    ViewBag.data3 = null;
                    ViewBag.nombre3 = null;
                }
                /*Termina el de tipo correo (3)*/

                /*Grafico 4 - Reporte Clientes Activos*/
                try
                {

                    int cantidadActivos = rep.ListarReporteclientesactivos().Where(x => x.Username == Session["Username"].ToString()).Count();

                    var lista = rep.ListarReporteclientesactivos().Where(x => x.Username == Session["Username"].ToString());
                    var reporte = Mapper.Map<List<Models.Reporteclientesactivos>>(lista.Where(x => x.Username == Session["Username"].ToString()));


                    foreach (Models.Reporteclientesactivos a in reporte)
                    {
                        Console.WriteLine("Cliente:" + a.Nombre + " Cantidad:" + a.Cantidad);

                    }

                    List<DATA.Reporteclientesactivos> listareportes = rep.ListarReporteclientesactivos();
                    var reporteslistados = listareportes.Select(x => x.Id_Contacto).Distinct();

                    List<dataChart> listaReporte = new List<dataChart>();
                    foreach (var item in reporteslistados)
                    {
                        listaReporte.Add(new dataChart(rep.ListarReporteclientesactivos().Where(x => x.Username == Session["Username"].ToString() && x.Id_Contacto == item).Select(x => x.Cantidad).First(),
                            cont.ListarContactos().Where(x => x.Id_Contacto == item).Select(x => x.Nombre).First().ToString()));
                    }
                    ViewBag.group4 = cont.ListarContactos().Select(x => x.Nombre).Distinct();
                    ViewBag.data4 = listaReporte.ToList();
                    ViewBag.nombre4 = cantidadActivos;
                }
                catch (Exception e)
                {
                    ViewBag.group4 = null;
                    ViewBag.data4 = null;
                    ViewBag.nombre4 = 0;
                }
                /*Fin de Grafico 4*/

                /*Table Contactos*/
                try
                {
                    var lista2 = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString());
                    var contactos2 = Mapper.Map<List<Models.Contactos>>(lista2);

                    return View(contactos2);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "No se ha podido mostrar la tabla");
                }
                
            }
            ViewBag.Rol = Session["Rol"].ToString();
            return View();
        }
           
            public ActionResult Logout()
            {
                try
                {
                    Session.Abandon();
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = ex.Message;
                     return View();
                }
                
            }


        }
    }
