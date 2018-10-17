using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DATA;
using TheOne.OrmLite.Core;
using TheOne.OrmLite.MySql;

namespace WiserSoft.DAL.Metodos
{
    public class MConfirmaciones : IConfirmaciones
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MConfirmaciones()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);

        }
        public void InsertarConfirmaciones(Confirmaciones confirmaciones)
        {
            _db = _conexion.Open();
            _db.Insert(confirmaciones);
        }
    }
}
