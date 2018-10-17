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
    public class MComunicaciones : IComunicaciones
    {

        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MComunicaciones()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public void ActualizarComunicaciones(Comunicaciones comunicaciones)
        {
            _db = _conexion.Open();
            _db.Update(comunicaciones);
        }

        public Comunicaciones BuscarComunicaciones(int id)
        {
            _db = _conexion.Open();
            return _db.Select<Comunicaciones>(x => x.Id_Comunicacion == id).FirstOrDefault();
        }

        public void EliminaComunicaciones(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Comunicaciones>(x => x.Id_Comunicacion == id); 
        }

        public void InsertarComunicaciones(Comunicaciones comunicaciones)
        {
            _db = _conexion.Open();
            _db.Insert(comunicaciones);
        }

        public List<Comunicaciones> ListarComunicaciones()
        {
            _db = _conexion.Open();
            return _db.Select <Comunicaciones>();
        }
    }
}
