using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IUsuarios
    {
        List<Usuarios> ListarUsuarios();
        Usuarios BuscarUsuarios(String username);
        void InsertarUsuarios(Usuarios usuarios);
        void ActualizaUsuarios(Usuarios usuarios);
        void EliminarUsuarios(String username);
        bool BuscarUsuarios(string username, string us_password);
    }
}
