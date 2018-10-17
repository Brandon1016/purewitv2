using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiserSoft.UI.Models
{
    public class Usuarios
    {
        [Required(ErrorMessage = "Nombre usuario es requerido")]
        [MinLength(5, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Nombre completo es requerido")]
        public String Fullname { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        public String Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password es requerido")]
        [Compare("Password", ErrorMessage = "Password no coinciden.")]
        public String ConfirmaPassowrd { get; set; }

        public DateTime Fecha_registro { get; set; }

        [Required(ErrorMessage = "Correo electrónico es requerido")]
        public String Correo { get; set; }
        public int Id_rol { get; set; }
    }
}