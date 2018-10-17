using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IContactos_Por_Lista
    {
        List<Contactos_Por_Listas> Listar();
        Contactos_Por_Listas BuscarContactos_Por_Listas(int id);
        void InsertarContactos_Por_Listas(Contactos_Por_Listas contactos_x_lista);
        void ActualizarContactos_Por_Listas(Contactos_Por_Listas contactos_x_lista);
        void EliminarContactos_Por_Listas(int id);
        List<ContactosListas> ListarC();
    }
}
