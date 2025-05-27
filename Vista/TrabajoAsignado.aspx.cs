using Seguridad_JSC.Datos;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
	public partial class TrabajoAsignado : System.Web.UI.Page
	{
        private ClCotizacionL logicaTrabajo = new ClCotizacionL();

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
            if (!IsPostBack)
            {
                CargarCotizacion();
            }
        }
        private void CargarCotizacion()
        {
            try
            {
                int idUsuarioT = int.Parse(LblidUsuario.Text);

                var trabajo = logicaTrabajo.MtdListaTrabajoTecnico(idUsuarioT);

                if (trabajo != null && trabajo.Rows.Count > 0) 
                {
                    int pageSize = 8;

                    if (!string.IsNullOrWhiteSpace(TxtNumeroColm.Text) && int.TryParse(TxtNumeroColm.Text, out int resultado))
                    {
                        if (resultado > 0)
                            pageSize = resultado;
                    }

                    PagedDataSource pgitems = new PagedDataSource
                    {
                        DataSource = trabajo.DefaultView, 
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
                        "Swal.fire('Sin datos', 'No Se Encontraron Trabajos Asignados.', 'info');", true);

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
        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarCotizacion();
        }
        private void FinalizarCotizacion(int idCotizacion)
        {
            ClConexion Conexion = new ClConexion();
            SqlConnection conn = Conexion.MtdAbrirConexion(); 

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Cotizacion SET estado = 'Tecnico Completada' WHERE idCotizacion = @id", conn);
                cmd.Parameters.AddWithValue("@id", idCotizacion);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    $"Swal.fire('Error', 'No se pudo finalizar la cotización: {ex.Message}', 'error');", true);
            }
            finally
            {
                Conexion.MtdcerrarConexion();
            }
        }

        protected void btnVerCotizacion_Command(object sender, CommandEventArgs e)
        {
            string idCotizacion = e.CommandArgument.ToString();
            Response.Redirect("UsuarioCotizacion.aspx?idCotizacion=" + idCotizacion);
        }
        protected void bntFinalizar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            string idString = btn.CommandArgument;

            if (int.TryParse(idString, out int idCotizacion))
            {
                FinalizarCotizacion(idCotizacion);

               
                CargarCotizacion();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertSuccess",
                    "Swal.fire('Completado', 'La Tarea fue finalizada exitosamente.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    "Swal.fire('Error', 'No se pudo obtener el ID de la Tarea.', 'error');", true);
            }
        }
        private void ActualizarObservacionTrabajo(int idCotizacion, string observacion)
        {
            ClConexion Conexion = new ClConexion();
            SqlConnection connection = Conexion.MtdAbrirConexion();

            using (SqlCommand cmd = new SqlCommand("UPDATE Cotizacion SET observacionesTrabajo = @obs WHERE idCotizacion = @id", connection))
            {
                cmd.Parameters.AddWithValue("@obs", observacion);
                cmd.Parameters.AddWithValue("@id", idCotizacion);

               
                cmd.ExecuteNonQuery();
                Conexion.MtdcerrarConexion();
            }
        }

        protected void btnObservacionesT_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idCotizacion = Convert.ToInt32(btn.CommandArgument);

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox txtObservacion = (TextBox)item.FindControl("txtObservacionTrabajo");

            if (txtObservacion != null)
            {
                string observacionNueva = txtObservacion.Text.Trim();
                ActualizarObservacionTrabajo(idCotizacion, observacionNueva);

                CargarCotizacion(); // recarga los datos para reflejar el cambio

                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "Swal.fire('Guardado', 'Observación actualizada correctamente.', 'success');", true);
            }
        }

    }
}