using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloAAEF;

public partial class LogueoCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GeneroContext();
            Session["Cliente"] = null;
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

    protected void BtnIniciar_Click(object sender, EventArgs e)
    {
        try
        {
            AAEntities contexto = (AAEntities)Session["Contexto"];
            Clientes unC = contexto.Clientes.FirstOrDefault(x => x.nroPasaporte == txtPasaporte.Text.Trim() && x.contrasenia == txtPass.Text.Trim());

            if (unC != null)
            {
                Session["Cliente"] = unC;
                Response.Redirect("~/HistoricoCompras.aspx");
            }
            else
                lblError.Text = "Credenciales incorrectas.";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}