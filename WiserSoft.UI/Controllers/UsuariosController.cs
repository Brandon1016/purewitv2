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
    public class UsuariosController : Controller
    {
        IUsuarios usuar;

        public UsuariosController()
        {
            usuar = new MUsuarios();
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            ViewBag.Mensaje = "no";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Usuarios usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Encripta el password
                    var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(usuarios.Password);
                    //Asigna la variable encriptada al objeto Password
                    usuarios.Password = passwordEncriptado;

                    var fechaRegistro = DateTime.Today.ToShortDateString();
                    usuarios.Fecha_registro = Convert.ToDateTime(fechaRegistro);

                    var rol = 1;
                    usuarios.Id_rol = Convert.ToInt32(rol);
               
                    var usuariosInsertar = Mapper.Map<DATA.Usuarios>(usuarios);
                    usuar.InsertarUsuarios(usuariosInsertar);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "si";
                ModelState.AddModelError("error", "No pudo ser registrado, intente con otro nombre de usuario.");
            }

            return View();
        }

         public ActionResult Profile()
         {

            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                string username = ViewBag.userId = Session["Username"];

                var lista = usuar.BuscarUsuarios(username);
                var usuarioBuscar = Mapper.Map<Models.Usuarios>(lista);

                //Decripta el password
                var passwordDecriptado = Encriptacion.Encriptacion.Decriptar(usuarioBuscar.Password);
                //Asigna la variable decriptada al objeto Password
                usuarioBuscar.Password = passwordDecriptado;
                usuarioBuscar.ConfirmaPassowrd = passwordDecriptado;

                return View(usuarioBuscar);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
         }

        [HttpPost]
        public ActionResult Profile(Models.Usuarios usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.userId = Session["Username"];
                    usuarios.Username = ViewBag.userId;

                    //Encripta el password
                    var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(usuarios.Password);
                    //Asigna la variable encriptada al objeto Password
                    usuarios.Password = passwordEncriptado;

                    var usuariosEditar = Mapper.Map<DATA.Usuarios>(usuarios);
                    usuar.ActualizaUsuarios(usuariosEditar);
                    
                }
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Profile");
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido actualizar");
                return RedirectToAction("Index");
            }

        }

    }
}