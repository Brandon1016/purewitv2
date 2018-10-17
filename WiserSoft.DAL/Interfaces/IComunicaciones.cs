using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IComunicaciones
    {
        List<Comunicaciones> ListarComunicaciones();
        Comunicaciones BuscarComunicaciones(int id);
        void InsertarComunicaciones(Comunicaciones comunicaciones);
        void ActualizarComunicaciones(Comunicaciones comunicaciones);
        void EliminaComunicaciones(int id);
    }
}
