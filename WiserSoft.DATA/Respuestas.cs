using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Respuestas
    {
        public int Id_Comunicacion { get; set; }
        public int Id_Contacto { get; set; }
        public string Mensaje { get; set; }
        public int Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombre { get; set; }
    }
}
