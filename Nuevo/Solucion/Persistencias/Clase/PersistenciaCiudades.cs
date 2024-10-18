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
    internal class PersistenciaCiudades :IPCiudades
    {
        private static PersistenciaCiudades instancia = null;
        private PersistenciaCiudades() { }
        public static PersistenciaCiudades GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaCiudades();
            return instancia;
        }
        internal Ciudades Buscar(string cod)
        {
            Ciudades unaC = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarCiudades", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cod", cod);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unaC = new Ciudades(cod, (string)datos["pais"], (string)datos["ciudad"]);
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
            return unaC;
        }
        public Ciudades BuscarActivas(string cod)
        {
            Ciudades unaC = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarCiudadesActivas", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cod", cod);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unaC = new Ciudades(cod, (string)datos["pais"], (string)datos["ciudad"]);
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
            return unaC;
        }
        public void AltaCiudades(Ciudades unaC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaCiudades", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unaC.CodigoC);
            comando.Parameters.AddWithValue("@pais", unaC.Pais);
            comando.Parameters.AddWithValue("@nom", unaC.Ciudad);


            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("Ya existe ese Ciudad.");
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
        public void BajaCiudades(Ciudades unaC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("BajaCiudades", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unaC.CodigoC);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("La Ciudades no existe, no se puede eliminar.");
                else if ((int)retorno.Value == -2)
                    throw new Exception("Error al eliminar el Ciudad.");
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
        public void ModificarCiudades(Ciudades unaC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ModificarCiudades", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@cod", unaC.CodigoC);
            comando.Parameters.AddWithValue("@pais", unaC.Pais);
            comando.Parameters.AddWithValue("@nom", unaC.Ciudad);


            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("No existe la ciudad a modificar.");
                else if ((int)retorno.Value == -2)
                    throw new Exception("No se puede modificar la ciudad.");

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
        public List<Ciudades> ListarCiudad()
        {
            List<Ciudades> lista = new List<Ciudades>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListarCiudades", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {

                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        lista.Add(new Ciudades((string)datos["codigoC"], (string)datos["pais"], (string)datos["ciudad"]));
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
