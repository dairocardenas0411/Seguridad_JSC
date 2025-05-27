using Seguridad_JSC.Datos;
using Seguridad_JSC.Logica;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class ListaCotizaciones : System.Web.UI.Page
    {
        private ClCotizacionL logicaCotizacion = new ClCotizacionL();

        private int CurrentPage
        {
            get => ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0;
            set => ViewState["CurrentPage"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null && Session["idRol"] != null)
            {
                LblidUsuario.Text = Session["idUsuario"].ToString();
            }
            else
            {
                Response.Redirect("index.aspx");
            }

            // Capturar postback por __doPostBack
            string eventTarget = Request["__EVENTTARGET"];
            if (!string.IsNullOrEmpty(eventTarget) && eventTarget.StartsWith("EliminarCotizacion"))
            {
                int id = int.Parse(eventTarget.Replace("EliminarCotizacion", ""));
                bool eliminado = EliminarCotizacion(id);

                if (eliminado)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "exito", "Swal.fire('Eliminado', 'Daros eliminados con éxito.', 'success');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "Swal.fire('Error', 'No se pudo eliminar.', 'error');", true);
                }

                CargarCotizacion();
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
                    int pageSize = 8;

                    if (!string.IsNullOrWhiteSpace(TxtNumeroColm.Text) && int.TryParse(TxtNumeroColm.Text, out int resultado))
                    {
                        if (resultado > 0)
                            pageSize = resultado;
                    }

                    PagedDataSource pgitems = new PagedDataSource
                    {
                        DataSource = cotizacion,
                        AllowPaging = true,
                        PageSize = pageSize,
                        CurrentPageIndex = CurrentPage
                    };

                    rptCotizaciones.DataSource = pgitems;
                    rptCotizaciones.DataBind();

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

        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarCotizacion();
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            if (int.TryParse(e.CommandArgument.ToString(), out int idCotizacion))
            {
                try
                {
                    bool resultado = EliminarCotizacion(idCotizacion);
                    if (resultado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('¡Eliminada!', 'La Solicitud ha sido eliminada correctamente.', 'success') " +
                            ".then(() => { window.location='../Vista/ListaCotizaciones.aspx'; });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('Error', 'No se pudo eliminar la cotización.', 'error');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        $"Swal.fire('Error', 'Ocurrió un error: {ex.Message}', 'error');", true);
                }
            }
        }

        private bool EliminarCotizacion(int idCotizacion)
        {
            ClConexion objConexion = new ClConexion();

            try
            {
                using (SqlConnection con = objConexion.MtdAbrirConexion())
                {
                    string query = "DELETE FROM Cotizacion WHERE idCotizacion = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idCotizacion);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                objConexion.MtdcerrarConexion();
            }
        }


    }
}
