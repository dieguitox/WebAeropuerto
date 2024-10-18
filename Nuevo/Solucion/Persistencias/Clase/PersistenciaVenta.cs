using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencias
{
    internal class PersistenciaVenta : IPVenta
    {
        private static PersistenciaVenta instancia = null;
        private PersistenciaVenta() { }
        public static PersistenciaVenta GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaVenta();
            return instancia;
        }
        public int AltaVenta(Ventas unaV)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaVenta", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@precio", unaV.Monto);
            comando.Parameters.AddWithValue("@codigo", unaV.Vue.CodigoV);
            comando.Parameters.AddWithValue("@pasaporte", unaV.Cli.NroPasaporte);
            comando.Parameters.AddWithValue("@usuario", unaV.Emp.Usuario);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            int resultado;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("Codigo del vuelo inexistente.");
                else if (resultado == -2)
                    throw new Exception("Numero de pasaporte inválido.");
                else if (resultado == -3)
                    throw new Exception("Error al realizar el proceso de venta.");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return unaV.NroTicket = resultado;
        }
        public Ventas BuscarVenta(int factura)
        {
            Ventas unV = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("MostrarVenta", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@factura", factura);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unV = new Ventas(factura, (double)datos["monto"], (DateTime)datos["fechaCompra"], PersistenciaCliente.GetInstancia().BuscarClienteActivo((string)datos["nroPasaporte"]),FabricaPersistencia.GetPersistenciaEmpleado().BuscarE((string)datos["usuario"]), PersistenciaVuelo.GetInstancia().BuscarVuelo((string)datos["codigoV"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return unV;
        }
        public List<Ventas> ListadoVenta()
        {
            Ventas unV = null;
            List<Ventas> lista = new List<Ventas>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListadoVenta", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        unV = new Ventas((int)datos["nroTicket"], (double)datos["monto"], (DateTime)datos["fechaCompra"], PersistenciaCliente.GetInstancia().BuscarClienteActivo((string)datos["nroPasaporte"]), FabricaPersistencia.GetPersistenciaEmpleado().BuscarE((string)datos["usuario"]), PersistenciaVuelo.GetInstancia().BuscarVuelo((string)datos["codigoV"]));
                        lista.Add(unV);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

    }
}
