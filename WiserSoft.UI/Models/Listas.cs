using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WiserSoft.UI.Models
{
    public class Listas
    {
        public int Id_Lista { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir el nombre de la lista")]
        public String Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir la descripción de la lista")]
        public String Descripcion { get; set; }
        public String Username { get; set; }
    }
}