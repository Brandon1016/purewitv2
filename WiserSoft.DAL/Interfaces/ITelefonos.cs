using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
   public interface ITelefonos
    {
        List<Telefonos> ListarTelefonos();
        Telefonos BuscarTelefonos(string numero);
        void InsertarTelefonos(Telefonos telefonos);
        void ActualizaTelefonos(Telefonos telefonos);
        void EliminarTelefonos(string numero);
    }
}
