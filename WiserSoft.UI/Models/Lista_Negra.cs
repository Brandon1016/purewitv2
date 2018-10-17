using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace WiserSoft.UI.Models
{
    public class Lista_Negra
    {
        public int Id_Lista_Negra { get; set; }
        [Required]
        public int Id_Contacto { get; set; }
    }
}