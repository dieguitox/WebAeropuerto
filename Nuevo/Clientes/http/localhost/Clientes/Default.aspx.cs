using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloAAEF;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GeneroContext();
                AAEntities contexto = (AAEntities)Session["Contexto"];
                Session["Departure"] = contexto.Vuelos.Where(x => x.fechaD > DateTime.Now).ToList();
                Session["Arrived"] = contexto.Vuelos.Where(x => x.fechaA > DateTime.Now).ToList();
                Session["FiltroD"] = Session["Departure"];
                Session["FiltrosA"] = Session["Arrived"];
                CargarDdls();

                Session["Cliente"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
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

    private void CargarDdls()
    {
        try
        {
            AAEntities contexto = (AAEntities)Session["Contexto"];
            List<Aeropuertos> listaAero = (from unA in contexto.Aeropuertos.ToList()
                                           orderby unA.nombreA
                                           select unA).ToList();

            ddlAeropuertos.DataSource = listaAero;
            ddlAeropuertos.DataTextField = "NombreA";
            ddlAeropuertos.DataBind();
            ddlAeropuertos.Items.Insert(0, "Seleccione Aeropuerto");

            MostrarFiltro((List<Vuelos>)Session["Departure"], (List<Vuelos>)Session["Arrived"]);

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            AAEntities contexto = (AAEntities)Session["Contexto"];

            Session["FiltroD"] = contexto.Vuelos.Where(x => x.fechaD > DateTime.Now).ToList();
            Session["FiltrosA"] = contexto.Vuelos.Where(x => x.fechaA > DateTime.Now).ToList();

            if (ddlAeropuertos.SelectedIndex != 0)
            {
                Session["FiltroD"] = (from unV in (List<Vuelos>)Session["FiltroD"]
                                      where unV.Aeropuertos.nombreA == ddlAeropuertos.SelectedValue
                                      select unV).ToList();
            }

            if (!(string.IsNullOrWhiteSpace(txtFecha.Text)))
            {
                Session["FiltroD"] = (from unV in (List<Vuelos>)Session["FiltroD"]
                                      where Convert.ToDateTime(unV.fechaD.ToString()) == Convert.ToDateTime(txtFecha.Text)
                                      select unV).ToList();
            }

            if (ddlAeropuertos.SelectedIndex != 0)
            {
                Session["FiltrosA"] = (from unV in (List<Vuelos>)Session["FiltrosA"]
                                       where unV.Aeropuertos1.nombreA == ddlAeropuertos.SelectedValue
                                       select unV).ToList();
            }

            if (!(string.IsNullOrWhiteSpace(txtFecha.Text)))
            {
                Session["FiltrosA"] = (from unV in (List<Vuelos>)Session["FiltrosA"]
                                       where Convert.ToDateTime(unV.fechaA.ToString()) == Convert.ToDateTime(txtFecha.Text)
                                       select unV).ToList();
            }

            MostrarFiltro((List<Vuelos>)Session["FiltroD"], (List<Vuelos>)Session["FiltrosA"]);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void MostrarFiltro(List<Vuelos> listadoDeparture, List<Vuelos> listadoArrived)
    {
        AAEntities contexto = (AAEntities)Session["Contexto"];

        var listaD = (from unV in listadoDeparture
                      orderby unV.fechaD
                      select new
                      {
                          Vuelo = unV.codigoV,
                          Fecha = unV.fechaD,
                          Aeropuerto = unV.Aeropuertos.nombreA,
                          Destino = unV.Aeropuertos.Ciudades.ciudad,
                          Pais = unV.Aeropuertos.Ciudades.pais,
                          Pasajes = contexto.Venta.Count(r => r.codigoV == unV.codigoV)
                      }).ToList();

        gvdDepartures.DataSource = listaD;
        gvdDepartures.DataBind();

        var listaA = (from unV in listadoArrived
                      orderby unV.fechaA
                      select new
                      {
                          Vuelo = unV.codigoV,
                          Fecha = unV.fechaA,
                          Proviene = unV.Aeropuertos1.nombreA,
                          Ciudad = unV.Aeropuertos1.Ciudades.ciudad,
                          Pais = unV.Aeropuertos1.Ciudades.pais,
                          Pasajes = contexto.Venta.Count(r => r.codigoV == unV.codigoV)
                      }).ToList();

        gvdArrived.DataSource = listaA;
        gvdArrived.DataBind();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            ddlAeropuertos.SelectedIndex = -1;
            txtFecha.Text = "";
            CargarDdls();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}