using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad_JSC.Datos;

namespace Seguridad_JSC.Vista
{
    public partial class UsuarioCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idCotizacion"] != null)
                {
                    int idCotizacion;
                    if (int.TryParse(Request.QueryString["idCotizacion"], out idCotizacion))
                    {
                        CargarCotizacion(idCotizacion);
                    }
                    else
                    {
                        lblMensaje.Text = "⚠ ID de Cotizacion no válido.";
                    }
                }
                else
                {
                    lblMensaje.Text = "⚠ No se recibió el ID de la Cotizacion.";
                }
            }
        }
        private void CargarCotizacion(int idCotizacion)
        {

            ClConexion connectionString = new ClConexion();

        }
    }
}