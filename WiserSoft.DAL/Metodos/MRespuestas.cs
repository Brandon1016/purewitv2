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
    public class MRespuestas : IRespuestas
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MRespuestas()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
             MySqlDialect.Provider);

        }

        public List<Respuestas> ListarComunicaciones()
        {
            _db = _conexion.Open();
            var results = _db.Select<Respuestas>(@"SELECT c.Id_Comunicacion, c.Id_Contacto, Mensaje, Estado, Fecha, cc.Nombre FROM Comunicaciones c, Contactos cc where c.Id_Contacto = cc.Id_Contacto");

            return results;
        }
    }
}
