using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;


namespace WiserSoft.DAL.Interfaces
{
    public interface IMensajes
    {
        List<Mensajes> ListarMensajes();
        Mensajes BuscarMensajes(int id);
        void InsertarMensajes(Mensajes mensajes);
        void ActualizaMensajes(Mensajes mensajes);
        void EliminarMnensajes(int id);
    }
}
