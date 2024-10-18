using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;
using Persistencias;

namespace Persistencias
{
    internal class PersistenciaVuelo : IPVuelo
    {
        private static PersistenciaVuelo instancia = null;
        private PersistenciaVuelo() { }
        public static PersistenciaVuelo GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaVuelo();
            return instancia;
        }
        public void AltaVuelos(Vuelos unV)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaVuelos", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            
            comando.Parameters.AddWithValue("@fechaS", unV.FechaD);
            comando.Parameters.AddWithValue("@fechaL", unV.FechaA);
            comando.Parameters.AddWithValue("@precio", unV.Precio);
            comando.Parameters.AddWithValue("@asientos", unV.CantAsientos);
            comando.Parameters.AddWithValue("@codA", unV.CodA.CodigoA);
            comando.Parameters.AddWithValue("@codB", unV.CodB.CodigoA);

            SqlParameter codigoVParam = new SqlParameter("@codigoV", SqlDbType.VarChar, 15)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(codigoVParam);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("El aeropuerto de Partida no es válido.");
                else if (resultado == -2)
                    throw new Exception("El aeropuerto de Llegada no es válido.");
                else if (resultado == -3)
                    throw new Exception("Error al crear el Vuelo.");
                else if (resultado == 1)
                {
                    unV.CodigoV = comando.Parameters["@codigoV"].Value.ToString();
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
        }
        public Vuelos BuscarVuelo(string cod)
        {
            Vuelos unV = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarVuelos", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codV", cod);
            SqlDataReader datos;

            try
            {
                conexion.Open();
                datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unV = new Vuelos(cod, (DateTime)datos["fechaD"], (DateTime)datos["fechaA"],(double)datos["precio"],(int)datos["cantAsientos"], PersistenciaAeropuerto.GetInstancia().BuscarActivo((string)datos["codigoA"]), PersistenciaAeropuerto.GetInstancia().BuscarActivo((string)datos["codigoB"]));
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

        public List<Vuelos> ListadoVuelos()
        {
            Vuelos unV = null;
            List<Vuelos> lista = new List<Vuelos>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListadoVuelos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {

                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        unV = new Vuelos((string)datos["codigoV"], (DateTime)datos["fechaD"], (DateTime)datos["fechaA"], (double)datos["precio"], (int)datos["cantAsientos"], PersistenciaAeropuerto.GetInstancia().BuscarActivo((string)datos["codigoA"]), PersistenciaAeropuerto.GetInstancia().BuscarActivo((string)datos["codigoB"]));
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
