using Seguridad_JSC.Datos;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
	public partial class HistorialTrabajo : System.Web.UI.Page
	{
        private ClCotizacionL logicaCotizacion = new ClCotizacionL();

        private int CurrentPage
        {
            get => ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0;
            set => ViewState["CurrentPage"] = value;
        }

        private bool MostrarTodo
        {
            get => ViewState["MostrarTodo"] != null ? (bool)ViewState["MostrarTodo"] : false;
            set => ViewState["MostrarTodo"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null && Session["idRol"] != null)
            {
                LblidUsuario.Text = Session["idUsuario"].ToString();
            }
            else
            {
                Response.Redirect("../index.aspx");
            }

           

            if (!IsPostBack)
            {
                CargarHistorial();
            }
        }
        private void CargarHistorial()
        {
            try
            {
                var cotizacion = logicaCotizacion.MtdListaHistorial(MostrarTodo);

                if (cotizacion != null && cotizacion.Count > 0)
                {
                    int pageSize = 8;

                    if (!MostrarTodo)
                    {
                        if (!string.IsNullOrWhiteSpace(TxtNumeroColm.Text) && int.TryParse(TxtNumeroColm.Text, out int resultado))
                        {
                            if (resultado > 0)
                                pageSize = resultado;
                        }
                    }

                    PagedDataSource pgitems = new PagedDataSource
                    {
                        DataSource = cotizacion,
                        AllowPaging = !MostrarTodo,
                        PageSize = pageSize,
                        CurrentPageIndex = CurrentPage
                    };

                    rptCotizaciones.DataSource = pgitems;
                    rptCotizaciones.DataBind();

                    btnPrev.Visible = !MostrarTodo;
                    btnNext.Visible = !MostrarTodo;
                    lblPageInfo.Visible = !MostrarTodo;

                    btnPrev.Enabled = !pgitems.IsFirstPage;
                    btnNext.Enabled = !pgitems.IsLastPage;
                    lblPageInfo.Text = $"Página {CurrentPage + 1} de {pgitems.PageCount}";
                }
                else
                {
                    rptCotizaciones.DataSource = null;
                    rptCotizaciones.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoDatos",
                        "Swal.fire('Sin datos', 'No se encontraron Datos disponibles.', 'info');", true);

                    btnPrev.Visible = false;
                    btnNext.Visible = false;
                    lblPageInfo.Visible = false;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    "Swal.fire('Error', 'Ocurrió un error al cargar los Datos.', 'error');", true);
            }
        }

        protected void btnVerTodo_Click(object sender, EventArgs e)
        {
            MostrarTodo = true;
            CurrentPage = 0;
            CargarHistorial();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                CargarHistorial();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            CargarHistorial();
        }

        protected void btnVerCotizacion_Command(object sender, CommandEventArgs e)
        {
            string idCotizacion = e.CommandArgument.ToString();
            Response.Redirect("UsuarioCotizacion.aspx?idCotizacion=" + idCotizacion);
        }

        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarHistorial();
        }


    }
}