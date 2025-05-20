using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class TecnicoSeguridad_JSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon(); // Elimina la sesión
            Session.Clear(); // Borra los datos de sesión
            Response.Redirect("../index.aspx"); // Redirige al index
        }
    }
}