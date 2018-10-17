using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Mensajes
    {
        [AutoIncrement]
        public int Id_Mensaje { get; set; }
        public String Username { get; set; }
        public String Cuerpo_Mensaje { get; set; }
        public int Id_Tipo { get; set; }
    }
}
