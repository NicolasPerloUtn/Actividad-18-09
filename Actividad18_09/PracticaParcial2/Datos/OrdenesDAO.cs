using PracticaParcial2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial2.Datos
{
    class OrdenesDAO : IOrdenesDAO
    {

        public DataTable ListarMateriales()
        {       
            SqlConnection conexion = new SqlConnection(@"");
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_MATERIALES";
            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Close();
            return dt;
        }

        public int ProximaOrden()
        {
            SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
            SqlConnection conexion = new SqlConnection(@"");
            SqlCommand comando = new SqlCommand();
            try
            {

                comando.Parameters.Clear();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_PROXIMA_ORDENN";
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                return (int)param.Value;

            }
            catch (Exception ex)
            {

                throw;

            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

            }
        }

        public bool EjecutarInsert(OrdenRetiro oOrdenRetiro)
        {
            SqlTransaction transaction = null;
            SqlConnection conexion = new SqlConnection(@"");
            bool ok = true;

            try
            {
                conexion.Open();
                transaction = conexion.BeginTransaction();

                SqlCommand cmdMaestro = new SqlCommand("SP_INSERTAR_ORDEN", conexion, transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Parameters.AddWithValue("@fecha", oOrdenRetiro.Responsable);
                cmdMaestro.Parameters.AddWithValue("@responsable", oOrdenRetiro.Responsable);

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@nro";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(parametro);
                cmdMaestro.ExecuteNonQuery();

                int ordenNro = (int)parametro.Value;
                int count = 1;

                foreach (DetalleOrden detalle in oOrdenRetiro.DetalleOrdenes)
                {
                    SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", conexion, transaction);
                    cmdDetalle.Parameters.AddWithValue("@nro_orden", ordenNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", count);
                    cmdDetalle.Parameters.AddWithValue("@codigo", detalle.Material.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                    count++;
                    cmdDetalle.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ok = false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return ok;

        }

       

    }
}
