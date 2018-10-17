using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Difusiones
    {
        public int Id_Difusion { get; set; }
        public String Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public String Username { get; set; }
        public DateTime Fecha_Activacion { get; set; }
        public int Id_Tipo_Mensaje { get; set; }
        public int Id_Estado { get; set; }
        public int Id_Mensaje { get; set; }
        public int Id_Lista { get;set; }
    }
}
