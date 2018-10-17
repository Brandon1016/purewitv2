using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
   public class Usuarios
    {
        public String Username { get; set; }
        public String Fullname { get; set; }
        public String Password { get; set; }
        public DateTime Fecha_registro { get; set; }
        public String Correo { get; set; }
        public int Id_rol { get; set; }
    }
}
