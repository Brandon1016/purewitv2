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
    public class MTipo_Difusiones : ITipo_Difusiones
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MTipo_Difusiones()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public List<Tipo_Difusiones> ListarTipoDifusiones()
        {
            _db = _conexion.Open();
            return _db.Select<Tipo_Difusiones>();
        }
    }
}
