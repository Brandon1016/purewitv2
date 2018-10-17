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
    public class MLista_Negra : ILista_Negra
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;
    
        public MLista_Negra()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              MySqlDialect.Provider);
        }

        public Lista_Negra BuscarListaNegra(int id_contacto)
        {
            _db = _conexion.Open();
            return _db.Select<Lista_Negra>(x => x.Id_Contacto == id_contacto).FirstOrDefault();
        }

        public void EliminaListaNegra(int id_contacto)
        {
            _db = _conexion.Open();
            _db.Delete<Lista_Negra>(x => x.Id_Contacto == id_contacto);
        }

        public void InsertarListaNegra(Lista_Negra lista_negra)
        {
            _db = _conexion.Open();
            _db.Insert(lista_negra);
        }

        public List<Lista_Negra> ListarListaNegra()
        {
            _db = _conexion.Open();
            return _db.Select<Lista_Negra>();
        }
    }
}
