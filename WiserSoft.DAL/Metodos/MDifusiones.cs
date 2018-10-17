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
    public class MDifusiones : IDifusiones
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MDifusiones()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }
        public void ActualizarDifusiones(Difusiones difusiones)
        {
            _db = _conexion.Open();
            _db.Update(difusiones);
        }

        public Difusiones BuscarDifusiones(int id)
        {
            _db = _conexion.Open();
            return _db.Select<Difusiones>(x => x.Id_Difusion == id).FirstOrDefault();
        }

        public void EliminarDifusiones(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Difusiones>(x => x.Id_Difusion == id);
        }

        public void InsertarDifusiones(Difusiones difusiones)
        {
            _db = _conexion.Open();
            _db.Insert(difusiones);
        }

        public List<Difusiones> ListarDifusines()
        {
            _db = _conexion.Open();
            return _db.Select < Difusiones>();
        }
        
        public List<Difusiones> CantidadDifusiones()
        {
            _db = _conexion.Open();
            return _db.Select<Difusiones>(@"Select count(Id_Estado),e.Descripcion From Difusiones d,Estados e Where d.Id_Estado = e.Id group by e.Descripcion");
        }

    }
}
