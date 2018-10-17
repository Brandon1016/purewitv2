using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IHistoriales
    {
        List<Historiales> ListarHistoriales();
        Historiales BuscarHistoriales(int id);
        void InsertarHistoriales(Historiales historiales);
        void ActualizarHistoriales(Historiales historiales);
        void EliminaHistoriales(int id);
    }
}
