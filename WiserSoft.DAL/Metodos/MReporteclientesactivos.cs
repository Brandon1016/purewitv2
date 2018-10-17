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
    public class MReporteclientesactivos : IReporteclientesactivos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MReporteclientesactivos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }

       
        public Reporteclientesactivos BuscarReporteclientesactivos(int id_Contacto)
        {
            _db = _conexion.Open();
            return _db.Select<Reporteclientesactivos>(x => x.Id_Contacto == id_Contacto).FirstOrDefault();
        }

       
        public List<Reporteclientesactivos> ListarReporteclientesactivos()
        {
            _db = _conexion.Open();
            return _db.Select<Reporteclientesactivos>();
        }
    }
}
