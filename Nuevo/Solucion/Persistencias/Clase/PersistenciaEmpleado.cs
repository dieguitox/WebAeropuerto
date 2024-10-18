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
    internal class PersistenciaEmpleado :IPEmpleados
    {
        private static PersistenciaEmpleado instancia = null;
        private PersistenciaEmpleado() { }
        public static PersistenciaEmpleado GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaEmpleado();
            return instancia;
        }
        public Empleados Logueo(string usuario, string pass)
        {
            Empleados unE = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("LogueoE", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@usu", usuario);
            comando.Parameters.AddWithValue("@pass", pass);
            //SqlTransaction transaccion = null;

            try
            {
                conexion.Open();

                //transaccion = conexion.BeginTransaction();
                //comando.Transaction = transaccion;

                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    unE = new EntidadesCompartidas.Empleados((string)datos["usuario"], (string)datos["nombre"], (string)datos["contrasenia"], (string)datos["cargo"]);
                }

                //datos.Close();

                //if (unE != null)
                //{
                //    PersistenciaEmpleado.UsuarioSQL(usuario, pass, transaccion);
                //}

                //transaccion.Commit();
            }
            catch (Exception ex)
            {
                //transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return unE;
        }
        public Empleados BuscarE(string unE)
        {
            Empleados Emp = null;
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand comando = new SqlCommand("BuscarE", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@usu", unE);

            try
            {
                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    datos.Read();
                    Emp = new Empleados(unE, (string)datos["nombre"], (string)datos["contrasenia"], (string)datos["cargo"]);
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
            return Emp;
        }

        internal static void UsuarioSQL(string usuario, string pass, SqlTransaction transaccion)
        {
            SqlCommand comando = new SqlCommand("UsuarioSQL", transaccion.Connection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Transaction = transaccion;

            comando.Parameters.AddWithValue("@usu", usuario);
            comando.Parameters.AddWithValue("@pass", pass);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                comando.ExecuteNonQuery();

                int codigo = Convert.ToInt32(retorno.Value);
                if (codigo == -1)
                    throw new Exception("No es un empleado.");
                else if (codigo == -2)
                    throw new Exception("Error en la transaccion.");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
