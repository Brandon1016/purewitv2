using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;


namespace WiserSoft.DAL.Interfaces
{
    public interface IContactos
    {
        List<Contactos> ListarContactos();
        List<Contactos> ListarContactos2(int lista);
        Contactos BuscarContactos(int id_contacto);
        void InsertarContactos(Contactos contactos);
        void ActualizaContactos(Contactos contactos);
        void EliminarContactos(int id_contacto);
    }
}
