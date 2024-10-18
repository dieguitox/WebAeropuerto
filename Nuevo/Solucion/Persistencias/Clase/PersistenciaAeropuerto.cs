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
    internal class PersistenciaAeropuerto : IPAeropuerto
    {
        private static PersistenciaAeropuerto instancia = null;
        private PersistenciaAeropuerto() { }
        public static PersistenciaAeropuerto GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaAeropuerto();
            return instancia;
        }
        internal Aeropuertos Buscar(string cod)
        {
            Aeropuertos unA = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarAeropuerto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cod", cod);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if(datos.HasRows)
                {
                    datos.Read();
                    unA = new Aeropuertos(cod, (string)datos["nombreA"], (string)datos["direccion"], (double)datos["impuestopartida"], (double)datos["ImpuestoLlegada"], PersistenciaCiudades.GetInstancia().Buscar((string)datos["codigoC"]));
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
            return unA;
        }
        public Aeropuertos BuscarActivo(string cod)
        {
            Aeropuertos unA = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarAeropuerto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cod", cod);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unA = new Aeropuertos(cod, (string)datos["nombreA"], (string)datos["direccion"], (double)datos["impuestopartida"], (double)datos["ImpuestoLlegada"], PersistenciaCiudades.GetInstancia().Buscar((string)datos["codigoC"]));
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
            return unA;
        }
        public void AltaAeropuertos(Aeropuertos unA)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaAeropuertos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unA.CodigoA);
            comando.Parameters.AddWithValue("@nom", unA.NombreA);
            comando.Parameters.AddWithValue("@dir", unA.Direccion);
            comando.Parameters.AddWithValue("@partida", unA.ImpuestoPar);
            comando.Parameters.AddWithValue("@llegada", unA.ImpuestoLle);
            comando.Parameters.AddWithValue("@ciu", unA.Ciudad.CodigoC);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("Ya existe ese Aeropuerto.");
                else if (resultado == -2)
                    throw new Exception("No existe esa ciudad.");
                else if (resultado == -3)
                    throw new Exception("Error inesperado al dar de alta el Aeropuerto.");

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
        public void BajaAeropuertos(Aeropuertos unA)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("BajaAeropuertos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unA.CodigoA);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("El Aeropuerto no existe, no se puede eliminar.");
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
        public void ModificarAeropuertos(Aeropuertos unA)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ModificarAeropuertos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unA.CodigoA);
            comando.Parameters.AddWithValue("@nom", unA.NombreA);
            comando.Parameters.AddWithValue("@dir", unA.Direccion);
            comando.Parameters.AddWithValue("@partida", unA.ImpuestoPar);
            comando.Parameters.AddWithValue("@llegada", unA.ImpuestoLle);
            comando.Parameters.AddWithValue("@codC", unA.Ciudad.CodigoC);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("No existe el Aeropuerto.");
                else if ((int)retorno.Value == -2)
                    throw new Exception("No existe la ciudad.");
                else if ((int)retorno.Value == -3)
                    throw new Exception("Error al dar de alta el Aeropuerto.");

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
        public List<Aeropuertos> ListadoAeropuertos()
        {
            Aeropuertos unA = null;
            List<Aeropuertos> lista = new List<Aeropuertos>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListadoAeropuertos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        unA = new Aeropuertos((string)datos["codigoA"], (string)datos["nombreA"], (string)datos["direccion"], (double)datos["impuestoPartida"], (double)datos["impuestoLlegada"], FabricaPersistencia.GetPersistenciaCiudades().BuscarActivas((string)datos["codigoC"]));
                        lista.Add(unA);
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
