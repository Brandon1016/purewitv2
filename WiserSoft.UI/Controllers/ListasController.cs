using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using WiserSoft.DATA;

namespace WiserSoft.UI.Controllers
{
    public class ListasController : Controller
    {
        IListas list;
        IUsuarios usa;
        IContactos cont;
        IContactos_Por_Lista contL;

        public ListasController()
        {
            list = new MListas();
            usa = new MUsuarios();
            cont = new MContactos();
            contL = new MContactos_Por_Lista();
        }
        // GET: Listas
        public ActionResult Index()
        {
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                ViewBag.userId = Session["Username"];

                var lista = list.ListarListas().Where(x => x.Username == Session["Username"].ToString());
                var listas = Mapper.Map<List<Models.Listas>>(lista);
                return View(listas);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
           
        }

        public ActionResult Create()
        {
            ViewBag.userId = Session["Username"];
            ViewBag.Rol = Session["Rol"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Listas listas)
        {
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                ViewBag.userId = Session["Username"];
                listas.Username = ViewBag.userId;

                if (ModelState.IsValid)
                {
                    var listaInsertar = Mapper.Map<DATA.Listas>(listas);
                    list.InsertarListas(listaInsertar);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido crear la lista.");
                return View();
            }
        }

        public ActionResult Edit(int id_lista)
        {
            ViewBag.Rol = Session["Rol"].ToString();

            try
            {
                var lista = list.BuscarListas(id_lista);
                var listaBuscar = Mapper.Map<Models.Listas>(lista);
                return View(listaBuscar);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Listas lista)
        {
            try
            {
                ViewBag.userId = Session["Username"];
                lista.Username = ViewBag.userId;

                if (ModelState.IsValid)
                {
                    var listaEditar = Mapper.Map<DATA.Listas>(lista);
                    list.ActualizarLista(listaEditar);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido actualizar");
                return View();
            }
           
        }

        public ActionResult Delete(int id_lista)
        {
            try
            {
                list.EliminarLista(id_lista);
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido eliminar");
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Contactos(int id_lista)
        {
            ViewBag.Lista = id_lista;
            ViewBag.Rol = Session["Rol"].ToString();
            return View("Contactos");
        }

        [HttpGet]
        public JsonResult ContactosV(int idLista)
        {
            ViewBag.userId = Session["Username"];

                var lista = cont.ListarContactos2(idLista).Where(x => x.Username == Session["Username"].ToString());
                var listas = Mapper.Map<List<Models.Contactos>>(lista);

               return Json(listas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insertar(int contacto, int lista)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var datos = new Contactos_Por_Listas
                    {
                        Id_contacto_lista = 0,
                        Id_contacto = contacto,
                        Id_Lista = lista,
                    };

                    var contactolistaInsertar = Mapper.Map<DATA.Contactos_Por_Listas>(datos);
                    contL.InsertarContactos_Por_Listas(contactolistaInsertar);

                    ViewBag.Rol = Session["Rol"].ToString();
                    return RedirectToAction("Index");
                    
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Contactos");
            }

            return View();
        }


        public ActionResult Eliminar(int id_listaC)
        {
            try
            {
                contL.EliminarContactos_Por_Listas(id_listaC);
                return RedirectToAction("Contactos");
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido eliminar");
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Contactos");
            }

        }

        [HttpGet]
        public JsonResult ListarContactos(int idLista)
        {


            var lista = contL.ListarC().Where(x => x.Id_Lista == idLista);
            var listas = Mapper.Map<List<Models.ContactosListas>>(lista);
            return Json(listas, JsonRequestBehavior.AllowGet);
         
        }

    }
}