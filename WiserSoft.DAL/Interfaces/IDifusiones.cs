using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IDifusiones
    {
        List<Difusiones> ListarDifusines();
        Difusiones BuscarDifusiones(int id);
        void InsertarDifusiones(Difusiones difusiones);
        void ActualizarDifusiones(Difusiones difusiones);
        void EliminarDifusiones(int id);
        List<Difusiones> CantidadDifusiones();
    }
}
