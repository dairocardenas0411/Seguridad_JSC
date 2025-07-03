using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (!IsPostBack)
            {
                if (Session["idRol"] == null)
                {
                    Response.Redirect("../index.aspx");
                    return;
                }

                string userRole = Session["rol"]?.ToString() ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetUserRole", $"var userRole = '{userRole}'; showMenuBasedOnRole();", true);
            }

            // Este código puede ir fuera de !IsPostBack para que siempre se actualice
            btnPerfil.CommandArgument = Session["idUsuario"]?.ToString();

            if (Session["nombreUsuario"] != null)
            {
                lblnombresApellidos.Text = Session["nombreUsuario"].ToString();
                LblidUsuario.Text = Session["idUsuario"]?.ToString();

                string nombreImagen = Session["foto"]?.ToString();
                imgUsuario.ImageUrl = "~/Vista/Recursos/" + (!string.IsNullOrEmpty(nombreImagen) ? nombreImagen : "user.png");

                string nombreImagenDrop = Session["foto"]?.ToString();
                imgUsuarioDropdown.ImageUrl = "~/Vista/Recursos/" + (!string.IsNullOrEmpty(nombreImagen) ? nombreImagen : "user.png");

                lblNombreDropdown.Text = Session["nombreUsuario"].ToString();
                lblRol.Text = Session["nombreRol"].ToString(); 
            }
            else
            {
                lblnombresApellidos.Text = "Invitado";
                imgUsuario.ImageUrl = "~/Vista/Recursos/user.png";
            }
        }


        protected void btnPerfil_Command(object sender, CommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument); 
            ClUsuarioL logica = new ClUsuarioL();
            DataTable datos = logica.MtdDatosPerfil(idUsuario);

            if (datos.Rows.Count > 0)
            {
                Context.Items["datosPerfil"] = datos;
                Server.Transfer("~/Vista/Perfil.aspx", true); 
            }
        }
        public void ActualizarDatosUsuario()
        {
            if (Session["nombreUsuario"] != null)
            {
                lblnombresApellidos.Text = Session["nombreUsuario"].ToString();
                LblidUsuario.Text = Session["idUsuario"]?.ToString();

                string nombreImagen = Session["foto"] != null ? Session["foto"].ToString() : "user.png";
                imgUsuario.ImageUrl = "~/Vista/Recursos/" + nombreImagen;
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