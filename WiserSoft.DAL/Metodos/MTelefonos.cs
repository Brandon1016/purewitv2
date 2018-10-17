using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;
using WiserSoft.DAL.Interfaces;
using System.Data;
using TheOne.OrmLite.Core;
using TheOne.OrmLite.MySql;

namespace WiserSoft.DAL.Metodos
{
    public class MTelefonos : ITelefonos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MTelefonos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }

        public void ActualizaTelefonos(Telefonos telefonos)
        {
            _db = _conexion.Open();
            _db.Update(telefonos);
        }

        public Telefonos BuscarTelefonos(string numero)
        {
            _db = _conexion.Open();
            return _db.Select<Telefonos>(x => x.Numero == numero).FirstOrDefault();
        }

        public void EliminarTelefonos(string numero)
        {
            _db = _conexion.Open();
            _db.Delete<Telefonos>(x => x.Numero == numero);
        }

        public void InsertarTelefonos(Telefonos telefonos)
        {
            _db = _conexion.Open();
            _db.Insert(telefonos);
        }

        public List<Telefonos> ListarTelefonos()
        {
            _db = _conexion.Open();
            return _db.Select<Telefonos>();
        }
    }
}
