using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
   public interface IListas
    {
        List<Listas> ListarListas();
        Listas BuscarListas(int id);
        void InsertarListas(Listas listas);
        void ActualizarLista(Listas listas);
        void EliminarLista(int id);
    }
}
