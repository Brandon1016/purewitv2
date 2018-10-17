using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Telefonos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe de escribir el número.")]
        public String Numero { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione el usuario al que desea asignar.")]
        public String Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir el id de la cuenta.")]
        public String Account_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe escribir el token de autenticación.")]
        public String Authtoken { get; set; }
    }
}