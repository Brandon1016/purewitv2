using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class ContactosController : Controller
    {

        IContactos cont;
        IUsuarios usa;
        ILista_Negra list_negra;

        public ContactosController()
        {
            cont = new MContactos();
            usa = new  MUsuarios();
            list_negra = new MLista_Negra();
        }

        // GET: Contactos
        public ActionResult Index()
        {
            try
            {
                ViewBag.Rol = Session["Rol"].ToString();
                ViewBag.userId = Session["Username"];

                var lista = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString());
                var contactos = Mapper.Map<List<Models.Contactos>>(lista);

                List<DATA.Lista_Negra> listaNegra = list_negra.ListarListaNegra();
                var lista_Negra = Mapper.Map<List<Models.Lista_Negra>>(listaNegra);

                foreach (Models.Contactos a in contactos)
                {
                    var varas = lista_Negra.Where(x => x.Id_Contacto == a.Id_Contacto).FirstOrDefault();
                    if (varas != null)
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
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
        }
        
        public ActionResult Create()
        {
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {

                ViewBag.userId = Session["Username"];
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Create(Models.Contactos contactos)
        {
            try
            {
                ViewBag.Rol = Session["Rol"].ToString();

                bool canConvert = true;
                if (contactos.Numero != null)
                {
                    for (int i = 0; i < contactos.Numero.Length; i++)
                    {
                        int numero;
                        bool puede = int.TryParse("" + contactos.Numero[i], out numero);
                        if (puede != true)
                        {
                            canConvert = false;
                            break;
                        }
                    }
                }
                else
                {
                    canConvert = false;
                }

                if (canConvert != true)
                {
                    ModelState.AddModelError("errornumero", "Revise el número por favor.");
                }

                if (ModelState.IsValid)
                {
                    ViewBag.userId = Session["Username"];
                    contactos.Username = ViewBag.userId;
                    var contactoInsertar = Mapper.Map<DATA.Contactos>(contactos);
                    cont.InsertarContactos(contactoInsertar);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
                return View();
            }
            
            return View();
        }

        
        public ActionResult Edit(int Id_Contacto)
        {
            try
            {
                var contacto = cont.BuscarContactos(Id_Contacto);
                var contactoBuscar = Mapper.Map<Models.Contactos>(contacto);
                return View(contactoBuscar);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
           

            ViewBag.Rol = Session["Rol"].ToString();
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Contactos cliente)
        {
            ViewBag.userId = Session["Username"];

            try
            {
                
                bool canConvert = true;
                if (cliente.Numero != null)
                {
                    for (int i = 0; i < cliente.Numero.Length; i++)
                    {
                        int numero;
                        bool puede = int.TryParse("" + cliente.Numero[i], out numero);
                        if (puede != true)
                        {
                            canConvert = false;
                            break;
                        }
                    }
                }
                else
                {
                    canConvert = false;
                }

                if (canConvert != true)
                {
                    ModelState.AddModelError("errornumero", "Revise el número por favor.");
                }

                if (ModelState.IsValid)
                {
                    cliente.Username = ViewBag.userId;
                    var contactoEditar = Mapper.Map<DATA.Contactos>(cliente);
                    cont.ActualizaContactos(contactoEditar);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }


        public ActionResult Delete(int id_contacto)
        {
            try
            {
                cont.EliminarContactos(id_contacto);
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            
        }


        public ActionResult EditListaNegra(int Id_Contacto)
        {
            try
            {
                var contacto = cont.BuscarContactos(Id_Contacto);
                var contactoBuscar = Mapper.Map<Models.Contactos>(contacto);
                int idContacto = contactoBuscar.Id_Contacto;

                var listaNegra = list_negra.BuscarListaNegra(Id_Contacto);
                var listaNegraBuscar = Mapper.Map<Models.Lista_Negra>(listaNegra);

                if (listaNegraBuscar == null)
                {
                    var contactos = new Models.Lista_Negra();
                    var contactoListaNegra = Mapper.Map<DATA.Lista_Negra>(contactos);

                    contactoListaNegra.Id_Contacto = idContacto;
                    list_negra.InsertarListaNegra(contactoListaNegra);
                }
                else
                {
                    int idContactoListaNegra = listaNegraBuscar.Id_Contacto;
                    if (idContacto == idContactoListaNegra)
                    {
                        list_negra.EliminaListaNegra(idContacto);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "Ha ocurrido un error");
                return RedirectToAction("Index");
            }

            ViewBag.Rol = Session["Rol"].ToString();
            return RedirectToAction("Index");


        }

        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            Console.Write("Entra......");
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            Models.Contactos contac = new Models.Contactos();
                            if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value != null && workSheet.Cells[rowIterator, 4].Value != null)
                            {
                                contac.Numero = workSheet.Cells[rowIterator, 1].Value.ToString();
                                contac.Nombre = workSheet.Cells[rowIterator, 2].Value.ToString();
                                contac.Detalle = workSheet.Cells[rowIterator, 3].Value.ToString();
                                contac.Correo = workSheet.Cells[rowIterator, 4].Value.ToString();
                                contac.Username = Session["Username"].ToString();

                                bool canConvert = true;
                                if (contac.Numero != null)
                                {
                                    for (int i = 0; i < contac.Numero.Length; i++)
                                    {
                                        int numero;
                                        bool puede = int.TryParse("" + contac.Numero[i], out numero);
                                        if (puede != true)
                                        {
                                            canConvert = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    canConvert = false;
                                }

                                if (canConvert == true)
                                {

                                    if (TryValidateModel(contac))
                                    {
                                        var contactoInsertar = Mapper.Map<DATA.Contactos>(contac);
                                        cont.InsertarContactos(contactoInsertar);
                                    }//no es valido el contacto
                                }//no es un numero
                            }//no tienen valores
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }


    }
}
