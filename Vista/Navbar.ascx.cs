using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        private readonly ClLoginL usuarioLogica = new ClLoginL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null && Session["idRol"] != null)
            {
                int idRol;
                if (int.TryParse(Session["idRol"].ToString(), out idRol))
                {
                    RedirectUser(idRol);
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsuario == null || txtPassword == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
       "alert('¡Error! No se encontraron los campos de usuario y contraseña.');", true);

                return;
            }

            ClLoginE usuario = new ClLoginE
            {
                email = txtUsuario.Text.Trim(),
                password = txtPassword.Text.Trim()
            };

            var usuarioAutenticado = usuarioLogica.MtdIngresoL(usuario);

            if (usuarioAutenticado != null)
            {
                
                Session["idUsuario"] = usuarioAutenticado.idUsuario;
                Session["email"] = usuarioAutenticado.email;
                Session["nombreUsuario"] = usuarioAutenticado.nombreUsuario;
                Session["idRol"] = usuarioAutenticado.idRol;

                RedirectUser(usuarioAutenticado.idRol);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire('¡Error!', 'Email o contraseña incorrectos.', 'error');", true);
            }
        }

        private void RedirectUser(int idRol)
        {
            string url = "";

            switch (idRol)
            {
                case 1:
                    url = "/Vista/admin.aspx";
                    break;
                case 2:
                    url = "/Vista/TecnicoSeguridad_JSC.aspx";
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "Swal.fire('¡Error!', 'No tienes un rol válido.', 'error');", true);
                    return;
            }

            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest(); 
        }
    }
}
