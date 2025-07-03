using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
	public partial class ListaTecnicos : System.Web.UI.Page
    {
        private ClUsuarioL logicaUsuario = new  ClUsuarioL();

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
                CargarTecnicos();
            }
        }

        
        private void CargarTecnicos()
        {
            try
            {
                var Tecnicos = logicaUsuario.MtdListaTecnicos();

                if (Tecnicos != null && Tecnicos.Count > 0)
                {
                    int pageSize = 8;

                    if (!string.IsNullOrWhiteSpace(TxtNumeroColm.Text) && int.TryParse(TxtNumeroColm.Text, out int resultado))
                    {
                        if (resultado > 0)
                            pageSize = resultado;
                    }

                    PagedDataSource pgitems = new PagedDataSource
                    {
                        DataSource = Tecnicos,
                        AllowPaging = true,
                        PageSize = pageSize,
                        CurrentPageIndex = CurrentPage
                    };

                    rptTecnicos.DataSource = pgitems;
                    rptTecnicos.DataBind();

                    btnPrev.Enabled = !pgitems.IsFirstPage;
                    btnNext.Enabled = !pgitems.IsLastPage;
                    lblPageInfo.Text = $"Página {CurrentPage + 1} de {pgitems.PageCount}";
                }
                else
                {
                    rptTecnicos.DataSource = null;
                    rptTecnicos.DataBind();

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

        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarTecnicos();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                CargarTecnicos();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            CargarTecnicos();
        }

        protected void btnEstado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            string idString = btn.CommandArgument;

            if (int.TryParse(idString, out int idUsuario))
            {
               CambiarEstado(idUsuario);


                CargarTecnicos();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertSuccess",
                    "Swal.fire('Completado', 'Estado Actualizado exitosamente.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    "Swal.fire('Error', 'No se pudo obtener el ID del Usuario.', 'error');", true);
            }
        }

        private void CambiarEstado(int idUsuario)
        {
            ClConexion Conexion = new ClConexion();
            SqlConnection conn = Conexion.MtdAbrirConexion();

            try
            {
                string estadoActual = "";
                SqlCommand cmdEstado = new SqlCommand("SELECT estado FROM Usuario WHERE idUsuario = @id", conn);
                cmdEstado.Parameters.AddWithValue("@id", idUsuario);

                object resultado = cmdEstado.ExecuteScalar();
                if (resultado != null)
                {
                    estadoActual = resultado.ToString();
                }

                string nuevoEstado = (estadoActual == "Activo") ? "Inactivo" : "Activo";

                SqlCommand cmdActualizar = new SqlCommand("UPDATE Usuario SET estado = @nuevoEstado WHERE idUsuario = @id", conn);
                cmdActualizar.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                cmdActualizar.Parameters.AddWithValue("@id", idUsuario);

                cmdActualizar.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    $"Swal.fire('Error', 'No se pudo cambiar el estado del usuario: {ex.Message}', 'error');", true);
            }
            finally
            {
                Conexion.MtdcerrarConexion();
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

    }
}