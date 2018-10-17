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
    public class MListas : IListas
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MListas()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public void ActualizarLista(Listas listas)
        {
            _db = _conexion.Open();
            _db.Update(listas);
        }

        public Listas BuscarListas(int id)
        {
            _db = _conexion.Open();
            return _db.Select < Listas>(x => x.Id_Lista == id).FirstOrDefault();
        }

        public void EliminarLista(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Listas>(x => x.Id_Lista == id);
        }

        public void InsertarListas(Listas listas)
        {
            _db = _conexion.Open();
            _db.Insert(listas);
        }

        public List<Listas> ListarListas()
        {
            _db = _conexion.Open();
            return _db.Select<Listas>();
        }
    }
}
