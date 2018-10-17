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
    public class MContactos : IContactos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MContactos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);

        }

        public void ActualizaContactos(Contactos contactos)
        {
            _db = _conexion.Open();
            _db.Update(contactos);
        }

        public Contactos BuscarContactos(int id_contacto)
        {
            _db = _conexion.Open();
            return _db.Select<Contactos>(x => x.Id_Contacto == id_contacto).FirstOrDefault();
        }

        public void EliminarContactos(int id_contacto)
        {
            _db = _conexion.Open();
            _db.Delete<Contactos>(x => x.Id_Contacto == id_contacto);
        }

        public void InsertarContactos(Contactos contactos)
        {
            _db = _conexion.Open();
            _db.Insert(contactos);
        }

        public List<Contactos> ListarContactos()
        {
            _db = _conexion.Open();
            return _db.Select<Contactos>();
        }

        public List<Contactos> ListarContactos2(int lista)
        {
            _db = _conexion.Open();
            return _db.Select<Contactos>(@"select * from Contactos where  Id_Contacto not in (select Id_Contacto from Contactos_Por_Listas where Id_Lista = "+ lista + ")");
        }
    }
}
