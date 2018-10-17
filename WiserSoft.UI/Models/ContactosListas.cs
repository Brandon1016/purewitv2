using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class ContactosListas
    {
        public int Id_contacto_lista { get; set; }
        public int Id_contacto { get; set; }
        public int Id_Lista { get; set; }
        public String Numero { get; set; }
        public String Nombre { get; set; }
    }
}