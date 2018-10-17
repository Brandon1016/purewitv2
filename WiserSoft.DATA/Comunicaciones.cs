using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Comunicaciones
    {
        public int Id_Comunicacion { get; set; }
        public int Id_Contacto { get; set; }
        public String Numero_Twilio { get; set; }
        public String Mensaje { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
