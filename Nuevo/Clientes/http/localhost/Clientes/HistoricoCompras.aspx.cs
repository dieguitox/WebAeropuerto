using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloAAEF;

public partial class HistoricoCompras : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeneroContext();
            AAEntities contexto = (AAEntities)Session["Contexto"];
            Clientes unC = (Clientes)Session["Cliente"];

            var vuelos = (from unV in contexto.Venta.ToList()
                          where unV.nroPasaporte == unC.nroPasaporte
                          orderby unV.fechaCompra
                          select new
                          {
                              NroFactura = unV.nroTicket,
                              Fecha = unV.fechaCompra,
                              Vuelo = unV.codigoV,
                              Monto = unV.monto,
                              Empleado = unV.usuario
                          }).ToList();

            Session["Historico"] = vuelos;
            gdvHistorico.DataSource = Session["Historico"];
            gdvHistorico.DataBind();
        }
    }

    private void GeneroContext()
    {
        try
        {
            AAEntities contexto = new AAEntities();
            Session["Contexto"] = contexto;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gdvHistorico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AAEntities contexto = (AAEntities)Session["Contexto"];
            Clientes unC = (Clientes)Session["Cliente"];
            int codViaje =Convert.ToInt32(gdvHistorico.SelectedRow.Cells[1].Text);

            var listar = (from unF in contexto.Venta.ToList()
                          from unV in contexto.Vuelos.ToList()
                          from unA in contexto.Adquieren.ToList()
                          where unF.nroTicket == codViaje
                          where unV.codigoV == unF.codigoV
                          where unA.nroTicket == unF.nroTicket
                          select new
                          {
                              Viaje = unF.codigoV,
                              AeropuertoSalida = unV.Aeropuertos.codigoA,
                              CiudadSalida = unV.Aeropuertos.Ciudades.ciudad,
                              PaisSalida = unV.Aeropuertos.Ciudades.pais,
                              AeropuertoDestino = unV.Aeropuertos1.codigoA,
                              CiudadDestino = unV.Aeropuertos1.Ciudades.ciudad,
                              PaisDestino = unV.Aeropuertos1.Ciudades.pais,
                              Viajero = unA.Clientes.nombre,
                              Asiento = unA.nroAsiento
                          }).ToList();

            Session["Pasajes"] = listar;

            gdvPasaje.DataSource = Session["Pasajes"];
            gdvPasaje.DataBind();

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}