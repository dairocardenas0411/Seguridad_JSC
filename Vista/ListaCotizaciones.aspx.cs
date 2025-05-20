using Seguridad_JSC.Logica;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class ListaCotizaciones : System.Web.UI.Page
    {
        private ClCotizacionL logicaCotizacion = new ClCotizacionL();

        // Tamaño de página (número de registros por página)
        private int PageSize = 10;

        // Página actual guardada en ViewState para persistencia
        private int CurrentPage
        {
            get
            {
                return ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null)
            {
                LblidUsuario.Text = Session["idUsuario"].ToString();
            }
            else
            {
                Response.Redirect("index.aspx");
            }

            if (!IsPostBack)
            {
                CargarCotizacion();
            }
        }

        private void CargarCotizacion()
        {
            try
            {
                var cotizacion = logicaCotizacion.MtdListarCotizacionPendiente();

                if (cotizacion != null && cotizacion.Count > 0)
                {
                    PagedDataSource pgitems = new PagedDataSource();
                    pgitems.DataSource = cotizacion;
                    pgitems.AllowPaging = true;
                    pgitems.PageSize = PageSize;
                    pgitems.CurrentPageIndex = CurrentPage;

                    rptCotizaciones.DataSource = pgitems;
                    rptCotizaciones.DataBind();

                    // Habilitar o deshabilitar botones de paginación
                    btnPrev.Enabled = !pgitems.IsFirstPage;
                    btnNext.Enabled = !pgitems.IsLastPage;

                    lblPageInfo.Text = $"Página {CurrentPage + 1} de {pgitems.PageCount}";
                }
                else
                {
                    rptCotizaciones.DataSource = null;
                    rptCotizaciones.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoDatos",
                        "Swal.fire('Sin datos', 'No se encontraron Cotizaciones disponibles.', 'info');", true);

                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    lblPageInfo.Text = string.Empty;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    "Swal.fire('Error', 'Ocurrió un error al cargar las cotizaciones.', 'error');", true);
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                CargarCotizacion();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            CargarCotizacion();
        }

        protected void btnVerCotizacion_Command(object sender, CommandEventArgs e)
        {
            string idCotizacion = e.CommandArgument.ToString();
            Response.Redirect("UsuarioCotizacion.aspx?idCotizacion=" + idCotizacion);
           
        }
    }
}
