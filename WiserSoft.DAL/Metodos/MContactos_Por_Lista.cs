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
    public class MContactos_Por_Lista : IContactos_Por_Lista
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MContactos_Por_Lista()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }

        public void ActualizarContactos_Por_Listas(Contactos_Por_Listas contactos_x_lista)
        {
            _db = _conexion.Open();
            _db.Update(contactos_x_lista);
        }

        public Contactos_Por_Listas BuscarContactos_Por_Listas(int id)
        {
            _db = _conexion.Open();
            return _db.Select<Contactos_Por_Listas>(x => x.Id_contacto_lista == id).FirstOrDefault();
        }

        public void EliminarContactos_Por_Listas(int id)
        {
            _db = _conexion.Open();
            _db.Delete<Contactos_Por_Listas>(x => x.Id_contacto_lista == id);
        }

        public void InsertarContactos_Por_Listas(Contactos_Por_Listas contactos_x_lista)
        {
            _db = _conexion.Open();
            _db.Insert(contactos_x_lista);
        }

        public List<Contactos_Por_Listas> Listar()
        {
            _db = _conexion.Open();
            return _db.Select<Contactos_Por_Listas>();
        }

        public List<ContactosListas> ListarC()
        {
            _db = _conexion.Open();
            var results = _db.Select<ContactosListas>(@"SELECT l.Id_Lista ,Nombre, C.Numero FROM Contactos C, Contactos_Por_Listas l where C.Id_Contacto = l.Id_Contacto");

            return results;
        }
    }
}
