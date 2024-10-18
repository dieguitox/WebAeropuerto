using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencias
{
    public class PersistenciaAdquieren : IPAdquieren
    {
        private static PersistenciaAdquieren instancia = null;
        private PersistenciaAdquieren() { }
        public static PersistenciaAdquieren GetInstancia()
        {
            if (instancia == null)
                instancia = new PersistenciaAdquieren();
            return instancia;
        }
        public void AltaAdquieren(Adquieren adq)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaAdquieren", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ticket", adq.NroTicket.NroTicket);
            comando.Parameters.AddWithValue("@asiento", adq.NroAsiento);
            comando.Parameters.AddWithValue("@pasaporte", adq.NroPasaporte.NroPasaporte);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = (int)comando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                    throw new Exception("El cliente seleccionado no esta activo");
                else if (resultado == -2)
                    throw new Exception("El numero de ticket es incorrecto.");
                else if (resultado == -3)
                    throw new Exception("El asiento esta ocupado.");
                else if (resultado == -4)
                    throw new Exception("El avion esta lleno.No podemos vender ticket");
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
        public List<Adquieren> ListadoAdquieren()
        {
            Adquieren adq = null;
            List<Adquieren> lista = new List<Adquieren>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("ListadoAdquieren", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {

                conexion.Open();
                SqlDataReader datos = comando.ExecuteReader();

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {
                        adq = new Adquieren(FabricaPersistencia.GetPersistenciaCliente().BuscarClienteActivo((string)datos["nroPasaporte"]), FabricaPersistencia.GetPersistenciaVenta().ListadoVenta().Where(x => x.NroTicket == (int)datos["nroTicket"]).FirstOrDefault(), (int)datos["nroAsiento"]);
                        lista.Add(adq);
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
