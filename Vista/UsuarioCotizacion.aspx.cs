using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad_JSC.Datos;
using Seguridad_JSC.Logica;
using Seguridad_JSC.Entidades;

namespace Seguridad_JSC.Vista
{
    public partial class UsuarioCotizacion : System.Web.UI.Page
    {
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
                if (Request.QueryString["idCotizacion"] != null)
                {
                    int idCotizacion;
                    if (int.TryParse(Request.QueryString["idCotizacion"], out idCotizacion))
                    {
                        CargarTrabajo(idCotizacion);
                        CargarProductos();
                        CargarDatosProducto(idCotizacion);
                        CargarTecnicos();
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

        private void LlenarDatosModal(DataRow row)
        {
            hfIdTrabajo.Value = row["idCotizacion"].ToString();
            txtNombreCliente.Text = row["nombreCliente"].ToString();
            txtApellidoCliente.Text = row["apellidoCliente"].ToString();
            txtDocumento.Text = row["documento"].ToString();
            txtTelefono.Text = row["telefono"].ToString();
            txtEmail.Text = row["email"].ToString();
            txtDireccion.Text = row["direccionInstalacion"].ToString();
            // ... resto de campos del modal

            // Manejar DropDownLists
            string tecnicoId = row["idTecnico"].ToString();
            if (!string.IsNullOrEmpty(tecnicoId) && ddlTecnico.Items.FindByValue(tecnicoId) != null)
            {
                ddlTecnico.SelectedValue = tecnicoId;
            }

            // Valores decimales
            if (decimal.TryParse(row["valorInstalacion"].ToString(), out decimal valorInstalacion))
                txtValorInstalacion.Text = valorInstalacion.ToString("0.##");

            if (decimal.TryParse(row["cargosAdicionales"].ToString(), out decimal cargosAdicionales))
                txtCargosAdicionales.Text = cargosAdicionales.ToString("0.##");
        }

        private void CargarTrabajo(int idCotizacion)
        {
            ClCotizacionL cotizacionLogica = new ClCotizacionL();
            DataTable dt = cotizacionLogica.ObtenerDatosCotizacion(idCotizacion);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtCotizacionUnica = dt.Clone();

                dtCotizacionUnica.ImportRow(dt.Rows[0]);

                rptDatosCotizacion.DataSource = dtCotizacionUnica;
                rptBtn.DataSource = dtCotizacionUnica;
                rptDatosCotizacion.DataBind();
                rptBtn.DataBind();

                rptDatosProveedor.DataSource = dt;
                rptDatosProveedor.DataBind();

                LlenarDatosModal(dt.Rows[0]);
            }
        }
        private void CargarDatosProducto(int idCotizacion)
        {
            ClCotizacionL TrabajoLogica = new ClCotizacionL();
            DataTable DtProductos = TrabajoLogica.MtdDatosProductos(idCotizacion);

            if (DtProductos != null && DtProductos.Rows.Count > 0)
            {
                rptDatosProductos.DataSource = DtProductos;
                rptDatosProductos.DataBind();

            }
            else
            {
                rptDatosProductos.DataSource = null;
                rptDatosProductos.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertNoProductos",
                   "Swal.fire('Sin datos', 'No se encontraron Productos para esta cotización.', 'info');", true);
            }
        }

        private void CargarProductos()
        {
            try
            {
                ClListaProductoL oListaProductoL = new ClListaProductoL();
                List<ClProductoE> listaProducto = oListaProductoL.MtdListarProducto();

                if (listaProducto.Count > 0)
                {
                    ddlListaProductos.DataSource = listaProducto;
                    ddlListaProductos.DataTextField = "nombreProducto";
                    ddlListaProductos.DataValueField = "idProducto";
                    ddlListaProductos.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoProductos",
                       "Swal.fire('Sin datos', 'No se encontraron Productos disponibles.', 'info');", true);
                }
                ddlListaProductos.Items.Insert(0, new ListItem("--Seleccione un producto--", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al cargar los Productos: {ex.Message}');", true);
            }

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
                            "Swal.fire('¡Eliminada!', 'La cotización ha sido eliminada correctamente.', 'success').then(() => { window.location='../Vista/ListaCotizaciones.aspx'; });", true);
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
                        $"Swal.fire('Error', 'Ocurrió un error al eliminar la cotización: {ex.Message}', 'error');", true);
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

        protected void btnAceptar_Command(object sender, CommandEventArgs e)
        {
            if (int.TryParse(e.CommandArgument.ToString(), out int idCotizacion))
            {
                try
                {
                    string errorStock;
                    bool resultado = AceptarCotizacion(idCotizacion, out errorStock);

                    if (!string.IsNullOrEmpty(errorStock))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('Stock insuficiente', 'No puedes aceptar la cotización porque el stock es insuficiente.', 'warning');", true);
                        return;
                    }

                    if (resultado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('¡Aceptada!', 'La cotización ha sido aceptada correctamente.', 'success');", true);
                        CargarTrabajo(idCotizacion);
                        CargarDatosProducto(idCotizacion);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('Error', 'No se pudo aceptar la cotización.', 'error');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        $"Swal.fire('Error', 'Ocurrió un error al aceptar la cotización: {ex.Message}', 'error');", true);
                }
            }
        }


        public bool EsTecnico
        {
            get
            {
                return Session["idRol"] != null && Session["idRol"].ToString() == "2";
            }
        }

        private bool AceptarCotizacion(int idCotizacion, out string errorStock)
        {
            errorStock = null;
            ClConexion objConexion = new ClConexion();

            try
            {
                using (SqlConnection con = objConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AceptarTrabajo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idCotizacion", idCotizacion);

                        SqlParameter paramError = new SqlParameter("@errorStock", SqlDbType.NVarChar, 200)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(paramError);

                        cmd.ExecuteNonQuery();

                        errorStock = paramError.Value?.ToString();

                        return string.IsNullOrEmpty(errorStock);
                    }
                }
            }
            catch (Exception ex)
            {
                errorStock = "Error al ejecutar el procedimiento: " + ex.Message;
                return false;
            }
            finally
            {
                objConexion.MtdcerrarConexion();
            }
        }


        private void CargarTecnicos()
        {
            try
            {
                ClUsuarioL oListaTecnicoL = new ClUsuarioL();
                List<ClUsuarioE> ListaTecnicos = oListaTecnicoL.MtdListaTecnicos();

                if (ListaTecnicos.Count > 0)
                {
                    ddlTecnico.DataSource = ListaTecnicos;
                    ddlTecnico.DataTextField = "nombreUsuario";
                    ddlTecnico.DataValueField = "idUsuario";
                    ddlTecnico.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoProductos",
                       "Swal.fire('Sin datos', 'No se encontraron Tecnicos disponibles.', 'info');", true);
                }
                ddlTecnico.Items.Insert(0, new ListItem("--Seleccione un Tecnico--", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al cargar los Tecnicos: {ex.Message}');", true);
            }

        }
        protected void btnAbrirModal_Command(object sender, CommandEventArgs e)
        {
            int idCotizacion = int.Parse(e.CommandArgument.ToString());
            idCotizacion = Convert.ToInt32(hfIdTrabajo.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "$('#modalActualizar').modal('show');", true);
        }

        protected void btnActualizar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int idCotizacion;
                try
                {
                    idCotizacion = Convert.ToInt32(hfIdTrabajo.Value);
                }
                catch
                {
                    throw new Exception("El ID de la cotización no es válido.");
                }

                ClCotizacionE cotizacion = new ClCotizacionE();

                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
                    throw new Exception("El nombre del cliente es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtApellidoCliente.Text))
                    throw new Exception("El apellido del cliente es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtDocumento.Text) || !int.TryParse(txtDocumento.Text.Trim(), out int documento))
                    throw new Exception("Debe ingresar un número de documento válido.");

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                    throw new Exception("El teléfono es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                    throw new Exception("El email es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                    throw new Exception("La dirección de instalación es obligatoria.");



                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text.Trim(), out int cantidad))
                    throw new Exception("La cantidad es obligatoria y debe ser un número entero.");

                if (string.IsNullOrWhiteSpace(txtObservaciones.Text))
                    throw new Exception("Las observaciones son obligatorias.");

                if (string.IsNullOrWhiteSpace(txtValorInstalacion.Text) || !decimal.TryParse(txtValorInstalacion.Text.Trim(), out decimal valorInstalacion))
                    throw new Exception("Debe ingresar un valor de instalación válido.");

                if (string.IsNullOrWhiteSpace(txtCargosAdicionales.Text) || !decimal.TryParse(txtCargosAdicionales.Text.Trim(), out decimal cargosAdicionales))
                    throw new Exception("Debe ingresar un valor de cargos adicionales válido.");



                if (string.IsNullOrEmpty(ddlTecnico.SelectedValue) || string.IsNullOrEmpty(ddlListaProductos.SelectedValue))
                    throw new Exception("Debe seleccionar un técnico y un producto.");

                // Asignación de valores
                cotizacion.idCotizacion = idCotizacion;
                cotizacion.nombreCliente = txtNombreCliente.Text.Trim();
                cotizacion.apellidoCliente = txtApellidoCliente.Text.Trim();
                cotizacion.documento = documento;
                cotizacion.telefono = txtTelefono.Text.Trim();
                cotizacion.email = txtEmail.Text.Trim();
                cotizacion.direccionInstalacion = txtDireccion.Text.Trim();
                cotizacion.estado = ddlEstado.SelectedValue;
                cotizacion.cantidad = cantidad;
                cotizacion.observaciones = txtObservaciones.Text.Trim();
                cotizacion.valorInstalacion = valorInstalacion;
                cotizacion.cargosAdicionales = cargosAdicionales;
                cotizacion.tipoTrabajo = ddlTipoTrabajo.SelectedValue;
                cotizacion.observacionesTrabajo = txtObservacionesTecnico.Text.Trim();
                cotizacion.idUsuarioT = Convert.ToInt32(ddlTecnico.SelectedValue);
                cotizacion.idProducto = Convert.ToInt32(ddlListaProductos.SelectedValue);


                ClCotizacionL logica = new ClCotizacionL();
                bool resultado = logica.MtdActualizarTrabajo(cotizacion);

                if (resultado)
                {
                    string estado = cotizacion.estado;

                    if (estado == "Rechazada" || estado == "Completada")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                            "Swal.fire('Actualizado', 'El Tarea ha sido Rechazada O Completada.', 'success').then(() => { window.location.href = '../Vista/ListaCotizaciones.aspx'; });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success",
                            "Swal.fire('Actualizado', 'Datos Actualizados correctamente', 'success');", true);
                        CargarTrabajo(cotizacion.idCotizacion);
                    }
                }
                else
                {
                    throw new Exception("No se pudo actualizar la cotización en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }


    }

}