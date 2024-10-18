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
    internal class PersistenciaCliente :IPCliente
    {
        private static PersistenciaCliente instancia = null;
        private PersistenciaCliente() { }
        public static PersistenciaCliente GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaCliente();
            return instancia;
        }
        internal Clientes Buscar(string pasaporte)
        {
            Clientes unC = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarC", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@pasaporte", pasaporte);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unC = new Clientes(pasaporte, (string)datos["nombre"], (string)datos["pass"], (long)datos["nrotarjeta"]);
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
            return unC;
        }
        public Clientes BuscarClienteActivo(string pasaporte)
        {
            Clientes unC = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarClienteActivo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@pasaporte", pasaporte);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unC = new Clientes(pasaporte, (string)datos["nombre"], (string)datos["contrasenia"], (long)datos["nroTarjeta"]);
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
            return unC;
        }
        public void AltaClientes(Clientes unC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaClientes", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@pasaporte", unC.NroPasaporte);
            comando.Parameters.AddWithValue("@nom", unC.Nombre);
            comando.Parameters.AddWithValue("@pass", unC.Pass);
            comando.Parameters.AddWithValue("@tarj", unC.NroTarjeta);


            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("Ya existe ese Cliente.");

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
        public void BajaClientes(Clientes unC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("BajaClientes", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@pasaporte", unC.NroPasaporte);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("El pasaporte no existe,no se puede eliminar.");
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
        public void ModificarClientes(Clientes unC)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ModificarClientes", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@pasaporte", unC.NroPasaporte);
            comando.Parameters.AddWithValue("@nom", unC.Nombre);
            comando.Parameters.AddWithValue("@pass", unC.Pass);
            comando.Parameters.AddWithValue("@tarj", unC.NroTarjeta);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                if ((int)retorno.Value == -1)
                    throw new Exception("No existe el cliente a modificar.");
                else if ((int)retorno.Value == -2)
                    throw new Exception("No se puede modificar el cliente.");

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
        public List<Clientes> ListarClientes()
        {
            Clientes unCli = null;
            List<Clientes> lista = new List<Clientes>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListarClientes", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {

                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        unCli = new Clientes((string)datos["nroPasaporte"], (string)datos["nombre"], (string)datos["contrasenia"], (long)datos["nroTarjeta"]);
                        lista.Add(unCli);
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
