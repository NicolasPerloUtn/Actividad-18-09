using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial2.Dominio
{
    internal class OrdenRetiro
    {

        public int nroOrden { get; set; }

        public DateTime Fecha { get; set; }

        public string Responsable { get; set; }

        public List<DetalleOrden> DetalleOrdenes { get; set; }

        public OrdenRetiro()
        {
            DetalleOrdenes = new List<DetalleOrden>();
        }

        public void AgregarOrden(DetalleOrden detalle)
        {
            DetalleOrdenes.Add(detalle);
        }

        public void Eliminar(int indice)
        {
            DetalleOrdenes.RemoveAt(indice);
        }
    }
}
