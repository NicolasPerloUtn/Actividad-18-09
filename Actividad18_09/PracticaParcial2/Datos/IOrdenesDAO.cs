using PracticaParcial2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial2.Datos
{
    interface IOrdenesDAO
    {
        DataTable ListarMateriales();

        int ProximaOrden();
        bool EjecutarInsert(OrdenRetiro oOrdenRetiro);

    }
}
