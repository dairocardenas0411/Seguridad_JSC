using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreUsuario"] != null)
            {
                lblnombresApellidos.Text = Session["nombreUsuario"].ToString();
                LblidUsuario.Text = Session["idUsuario"].ToString();
            }
            else
            {
                lblnombresApellidos.Text = "Invitado";
            }

            if (!IsPostBack)
            {
                if (Session["idRol"] == null)
                {
                    Response.Redirect("../index.aspx");
                }

                
                string userRole = Session["rol"]?.ToString() ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetUserRole", $"var userRole = '{userRole}'; showMenuBasedOnRole();", true);
            }

        }

        protected void btncerrarSession_Click(object sender, EventArgs e)
        {
            Session.Abandon(); 
            Session.Clear(); 
            Response.Redirect("../index.aspx"); 
        }
    }
}