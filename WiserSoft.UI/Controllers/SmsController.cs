using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class SmsController : Controller
    {
        IComunicaciones com;
        IContactos con;
        ITelefonos tel;

        public SmsController()
        {
            com = new MComunicaciones();
            con = new MContactos();
            tel = new MTelefonos();
        }

        [HttpPost]
        public ActionResult Index()
        {
            try
            {
                string cuerpoDeRespuesta = Request.Form["Body"];
                string remitente = Request.Form["From"];
                string emisor = Request.Form["To"];
                var dateNow = DateTime.Now;
                DATA.Comunicaciones comunicaciones = new DATA.Comunicaciones();
                DATA.Contactos contacto = new DATA.Contactos();
                DATA.Telefonos telefono = new DATA.Telefonos();

                telefono = tel.ListarTelefonos().Where(x => x.Numero== emisor.Replace("+", "")).FirstOrDefault();
                contacto = con.ListarContactos().Where(x => x.Numero == remitente.Replace("+", "")).Where(x=> x.Username == telefono.Username).FirstOrDefault();
                comunicaciones.Id_Contacto = contacto.Id_Contacto;
                comunicaciones.Numero_Twilio = emisor.Replace("+", "");
                comunicaciones.Mensaje = cuerpoDeRespuesta;
                comunicaciones.Estado = 6;
                comunicaciones.Fecha = dateNow;
                com.InsertarComunicaciones(comunicaciones);

                var logMessage = $"\"{cuerpoDeRespuesta}\", \"{remitente}\", \"{emisor}\"";
                Trace.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Content("Handled");
        }
    }
}