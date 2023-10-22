using PracticaParcial2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial2.Datos
{
    internal class Gestor
    {
        private IOrdenesDAO dao;

        public DataTable ListarMateriales()
        {
            dao = new OrdenesDAO();
            return dao.ListarMateriales();
        }

        public int ProximaOrden()
        {
            dao = new OrdenesDAO();
            return dao.ProximaOrden();
        }

        public bool EjecutarInsert(OrdenRetiro oOrdenRetiro)
        {
            return dao.EjecutarInsert(oOrdenRetiro);
        }
    }
}
