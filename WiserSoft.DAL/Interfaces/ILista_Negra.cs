using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface ILista_Negra
    {
        List<Lista_Negra> ListarListaNegra();
        Lista_Negra BuscarListaNegra(int id_contacto);
        void InsertarListaNegra(Lista_Negra lista_negra);
        void EliminaListaNegra(int id_contacto);
    }
}
