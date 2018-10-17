using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;
using WiserSoft.DAL.Interfaces;
using TheOne.OrmLite.Core;
using TheOne.OrmLite.MySql;
using System.Data;

namespace WiserSoft.DAL.Metodos
{
    public class MMensajes : IMensajes
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MMensajes()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public void ActualizaMensajes(Mensajes mensajes)
        {
            _db = _conexion.Open();
            _db.Update(mensajes);
        }

        public Mensajes BuscarMensajes(int id)
        {
            _db = _conexion.Open();
            return _db.Select<Mensajes>(x => x.Id_Mensaje == id).FirstOrDefault();
        }

        public void EliminarMnensajes(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Mensajes>(x => x.Id_Mensaje == id);
        }

        public void InsertarMensajes(Mensajes mensajes)
        {
            _db = _conexion.Open();
            _db.Insert(mensajes);
        }

        public List<Mensajes> ListarMensajes()
        {
            _db = _conexion.Open();
            return _db.Select <Mensajes>();
        }
    }
}
