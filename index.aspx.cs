using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC
{
    public partial class index : System.Web.UI.Page
    {
        private readonly ClLoginL usuarioLogica = new ClLoginL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


    }
}
