using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Difusiones
    {
        public int Id_Difusion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir la descripción del mensaje.")]
        public String Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public String Username { get; set; }
        public DateTime Fecha_Activacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione el tipo de mensaje que desea enviar.")]
        public int Id_Tipo_Mensaje { get; set; }
        public String Des_tipo_Mensaje { get; set; }
        public int Id_Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione el mensaje que desea enviar.")]
        public int Id_Mensaje { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione la lista a la que desea enviar el mensaje.")]
        public int Id_Lista { get; set; }

        public String passwordCorreo { get; set; }

        public int CantidadDifEstado { get; set; }
        public String DescripcionEstado { get; set; }
    }
}