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
    public class MHistoriales : IHistoriales
    {

        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MHistoriales()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public void ActualizarHistoriales(Historiales historiales)
        {
            _db = _conexion.Open();
            _db.Update(historiales);
        }

        public Historiales BuscarHistoriales(int id)
        {
            _db = _conexion.Open();
            return _db.Select<Historiales>(x => x.Id == id).FirstOrDefault();
        }

        public void EliminaHistoriales(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Historiales>(x => x.Id == id);
        }

        public void InsertarHistoriales(Historiales historiales)
        {
            _db = _conexion.Open();
            _db.Insert(historiales);
        }

        public List<Historiales> ListarHistoriales()
        {
            _db = _conexion.Open();
            return _db.Select<Historiales>();
        }
    }
}
