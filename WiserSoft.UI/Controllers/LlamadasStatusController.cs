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

    public class LlamadasStatusController : Controller
    {
        IConfirmaciones con;
        IEstados est;
        public LlamadasStatusController()
        {
            con = new MConfirmaciones();
            est = new MEstados();
        }

        [HttpPost]
        public ActionResult Index()
        {
            // Log the message id and status
            var voiceSid = Request.Form["CallSid"];
            var llamadaStatus = Request.Form["CallStatus"];

            /*
                queued, ringing, in-progress, completed, busy, failed, no-answer
            */
            try
            {
                DATA.Estados estado = new DATA.Estados();
                if (llamadaStatus == "queued")
                {
                    estado = est.ListarEstados().Where(x => x.Descripcion == "En cola").First();
                }
                else
                {
                    if (llamadaStatus == "ringing")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Sonando").First();
                    }
                    else
                    if (llamadaStatus == "in-progress")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "En curso").First();
                    }
                    else
                    if (llamadaStatus == "completed")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Completo").First();
                    }
                    else
                    if (llamadaStatus == "busy")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Ocupado").First();
                    }
                    else
                    if (llamadaStatus == "failed")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Fallido").First();
                    }
                    else
                    if (llamadaStatus == "no-answer")
                    {
                        estado = est.ListarEstados().Where(x => x.Descripcion == "Sin respuesta").First();
                    }
                }


                DATA.Confirmaciones confirmaciones = new DATA.Confirmaciones();
                confirmaciones.Estado = estado.Id;
                confirmaciones.Message_id = voiceSid;

                con.InsertarConfirmaciones(confirmaciones);

                var logMessage = $"\"{estado.Id}\", \"{voiceSid}\", \"{llamadaStatus}\"";

                Trace.WriteLine(logMessage);
                return Content("Handled");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return Content("Handled");
            }
            
        }
    }
}