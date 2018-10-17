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

    public class MessageStatusController : Controller
    {
        IConfirmaciones con;
        IEstados est;
        public MessageStatusController()
        {
            con = new MConfirmaciones();
            est = new MEstados();
        }

        [HttpPost]
        public ActionResult Index()
        {
            try
            {
                // Log the message id and status
                var smsSid = Request.Form["SmsSid"];
                var messageStatus = Request.Form["MessageStatus"];

                DATA.Estados estado = new DATA.Estados();
                if (messageStatus == "sent")
                {
                    estado = est.ListarEstados().Where(x => x.Descripcion == "Enviado").First();
                }
                else
                {
                    if (messageStatus == "delivered")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Recibido").First();
                    }
                    else
                    if (messageStatus == "undelivered")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "No entregado").First();
                    }
                    else
                    if (messageStatus == "failed")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Ha fallado").First();
                    }
                    else
                    if (messageStatus == "queued")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "En cola").First();
                    }
                    else
                    if (messageStatus == "accepted")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Aceptado").First();
                    }
                    else
                    if (messageStatus == "sending")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Enviando").First();
                    }
                }

                DATA.Confirmaciones confirmaciones = new DATA.Confirmaciones();
                confirmaciones.Estado = estado.Id;
                confirmaciones.Message_id = smsSid;

                con.InsertarConfirmaciones(confirmaciones);

                var logMessage = $"\"{estado.Id}\", \"{smsSid}\", \"{messageStatus}\"";

                Trace.WriteLine(logMessage);
                return Content("Handled");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("Handled");
            }
        }
    }
}