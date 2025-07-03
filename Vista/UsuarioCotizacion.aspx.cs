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
                Response.Redirect("../index.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["idCotizacion"] != null)
                {
                    int idCotizacion;
                    if (int.TryParse(Request.QueryString["idCotizacion"], out idCotizacion))
                    {
                        CargarTrabajo(idCotizacion);
                        CargarDatosProducto(idCotizacion);
                        CargarTecnicos();
                        CargarGridProductosModal();
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
            txtObservaciones.Text = row["observaciones"].ToString();



            string tecnicoId = row["idTecnico"].ToString();
            if (!string.IsNullOrEmpty(tecnicoId) && ddlTecnico.Items.FindByValue(tecnicoId) != null)
            {
                ddlTecnico.SelectedValue = tecnicoId;
            }

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
                gvProductosCotizacion.DataSource = DtProductos;
                gvProductosCotizacion.DataBind();


            }
            else
            {
                rptDatosProductos.DataSource = null;
                rptDatosProductos.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertNoProductos",
                   "Swal.fire('Sin datos', 'No se encontraron Productos para esta cotización.', 'info');", true);
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
            if (int.TryParse(e.CommandArgument.ToString(), out int idCotizacion))
            {
                hfIdTrabajo.Value = idCotizacion.ToString();

                ClCotizacionL cotizacionLogica = new ClCotizacionL();
                DataTable dt = cotizacionLogica.ObtenerDatosCotizacion(idCotizacion);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CargarTecnicos();
                    LlenarDatosModal(dt.Rows[0]);
                }

                ScriptManager.RegisterStartupScript(
                     ScriptManager.GetCurrent(this),
                     this.GetType(),
                     "abrirModal",
                     "var modal = new bootstrap.Modal(document.getElementById('modalActualizar')); modal.show();",
                     true
                 );

            }
        }


        protected void btnActualizar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (!int.TryParse(hfIdTrabajo.Value, out int idCotizacion))
                    throw new Exception("El ID de la cotización no es válido.");

                if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
                    throw new Exception("El nombre del cliente es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtApellidoCliente.Text))
                    throw new Exception("El apellido del cliente es obligatorio.");

                if (!int.TryParse(txtDocumento.Text.Trim(), out int documento))
                    throw new Exception("Debe ingresar un número de documento válido.");

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                    throw new Exception("El teléfono es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                    throw new Exception("El email es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                    throw new Exception("La dirección de instalación es obligatoria.");

                if (ddlTecnico.SelectedValue == "0")
                    throw new Exception("Debe seleccionar un técnico.");

                if (!decimal.TryParse(txtValorInstalacion.Text.Trim(), out decimal valorInstalacion))
                    throw new Exception("Debe ingresar un valor de instalación válido.");

                if (!decimal.TryParse(txtCargosAdicionales.Text.Trim(), out decimal cargosAdicionales))
                    throw new Exception("Debe ingresar un valor de cargos adicionales válido.");

                ClCotizacionE cotizacion = new ClCotizacionE
                {
                    idCotizacion = idCotizacion,
                    nombreCliente = txtNombreCliente.Text.Trim(),
                    apellidoCliente = txtApellidoCliente.Text.Trim(),
                    documento = documento,
                    telefono = txtTelefono.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    direccionInstalacion = txtDireccion.Text.Trim(),
                    observaciones = txtObservaciones.Text.Trim(),
                    valorInstalacion = valorInstalacion,
                    cargosAdicionales = cargosAdicionales,
                    tipoTrabajo = ddlTipoTrabajo.SelectedValue,
                    observacionesTrabajo = txtObservacionesTecnico.Text.Trim(),
                    idUsuarioT = int.TryParse(ddlTecnico.SelectedValue, out int idTecnico) ? idTecnico : 0
                };

                ClCotizacionL logica = new ClCotizacionL();
                bool actualizado = logica.MtdActualizarTrabajo(cotizacion);

                if (actualizado)
                {
                    string script = @"Swal.fire('Actualizado', 'Datos actualizados correctamente.', 'success');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "actualizado", script, true);
                    CargarTrabajo(cotizacion.idCotizacion);
                    CargarDatosProducto(cotizacion.idCotizacion);
                }
                else
                {
                    throw new Exception("No se pudo actualizar la cotización.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }
        protected void btnAbrirModalProductos_Command(object sender, CommandEventArgs e)
        {
            if (int.TryParse(e.CommandArgument.ToString(), out int idCotizacion))
            {
                hfIdTrabajo.Value = idCotizacion.ToString();

                ClCotizacionL cotizacionLogica = new ClCotizacionL();
                DataTable dt = cotizacionLogica.ObtenerDatosCotizacion(idCotizacion);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CargarListaProductosModal();
                    CargarProductosAsociados(idCotizacion); // <<<<<< AQUI SE CARGAN LOS EXISTENTES
                    CargarGridProductosModal();
                }

                ScriptManager.RegisterStartupScript(
                      ScriptManager.GetCurrent(this),
                      this.GetType(),
                      "abrirModal",
                      "var modal = new bootstrap.Modal(document.getElementById('modalProductos')); modal.show();",
                      true
                );
            }
        }

        private void CargarListaProductosModal()
        {
            ClListaProductoL logicaProducto = new ClListaProductoL();
            List<ClProductoE> lista = logicaProducto.MtdListarProducto();

            ddlListaProductos.DataSource = lista;
            ddlListaProductos.DataTextField = "nombreProducto";
            ddlListaProductos.DataValueField = "idProducto";
            ddlListaProductos.DataBind();
            ddlListaProductos.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
        }
        private void CargarGridProductosModal()
        {
            gvProductosCotizacion.DataSource = ProductosSeleccionados;
            gvProductosCotizacion.DataBind();
        }



        private void CargarProductosAsociados(int idCotizacion)
        {
            ClCotizacionL logica = new ClCotizacionL();
            DataTable dt = logica.MtdDatosProductos(idCotizacion); // ← Asegúrate que devuelva el campo 'foto'

            var productos = new List<ProductoCotizacionTemp>();

            foreach (DataRow row in dt.Rows)
            {
                productos.Add(new ProductoCotizacionTemp
                {
                    IdProducto = Convert.ToInt32(row["idProducto"]),
                    NombreProducto = row["nombreProducto"].ToString(),
                    codigo = row["codigo"].ToString(),
                    Cantidad = Convert.ToInt32(row["cantidad"]),
                    Imagen = row["imagen"].ToString()
                });

            }

            ProductosSeleccionados = productos;
        }



        protected void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            int idProducto = int.Parse(ddlListaProductos.SelectedValue);
            string nombreProducto = ddlListaProductos.SelectedItem.Text;

            if (idProducto == 0)
            {
                MostrarAlerta("Advertencia", "Seleccione un producto válido.", "warning");
                return;
            }

            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad < 1)
            {
                MostrarAlerta("Advertencia", "Ingrese una cantidad válida.", "warning");
                return;
            }

            var lista = ProductosSeleccionados;

            // Verificar si ya existe el producto
            var productoExistente = lista.FirstOrDefault(p => p.IdProducto == idProducto);

            if (productoExistente != null)
            {
                // Si existe, sumar la cantidad
                productoExistente.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, lo agregamos
                var nuevo = new ProductoCotizacionTemp
                {
                    IdProducto = idProducto,
                    NombreProducto = nombreProducto,
                    Cantidad = cantidad
                };
                lista.Add(nuevo);
            }

            ProductosSeleccionados = lista;
            CargarGridProductosModal();
        }



        protected void gvProductosCotizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Refrescar desde ViewState actual
                var lista = ProductosSeleccionados;

                int index = Convert.ToInt32(e.CommandArgument);

                if (index >= 0 && index < lista.Count)
                {
                    lista.RemoveAt(index);
                    ProductosSeleccionados = lista;
                    CargarGridProductosModal();
                }
            }
        }



        private void MostrarAlerta(string titulo, string mensaje, string icono)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta",
                $"Swal.fire('{titulo}', '{mensaje}', '{icono}');", true);
        }
        protected void btnActualizarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(hfIdTrabajo.Value, out int idCotizacion))
                {
                    MostrarAlerta("Error", "ID de cotización inválido.", "error");
                    return;
                }

                var productosActualizados = ProductosSeleccionados
                    .Select(p => new ProductoCotizacion
                    {
                        idProducto = p.IdProducto,
                        cantidad = p.Cantidad
                    })
                    .ToList();

                if (productosActualizados.Count == 0)
                {
                    MostrarAlerta("Advertencia", "Debe agregar al menos un producto para actualizar.", "warning");
                    return;
                }

                ClCotizacionL logica = new ClCotizacionL();
                bool resultado = logica.MtdActualizarProductosCotizacion(idCotizacion, productosActualizados);

                if (resultado)
                {
                    MostrarAlerta("Éxito", "Productos actualizados correctamente.", "success");
                    CargarTrabajo(idCotizacion);
                    CargarDatosProducto(idCotizacion);
                    ProductosSeleccionados = new List<ProductoCotizacionTemp>();
                }
                else
                {
                    MostrarAlerta("Error", "No se pudo actualizar los productos.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Ocurrió un error al actualizar: " + ex.Message, "error");
            }
        }



        private List<ProductoCotizacionTemp> ProductosSeleccionados
        {
            get
            {
                if (ViewState["ProductosSeleccionados"] == null)
                    ViewState["ProductosSeleccionados"] = new List<ProductoCotizacionTemp>();
                return (List<ProductoCotizacionTemp>)ViewState["ProductosSeleccionados"];
            }
            set => ViewState["ProductosSeleccionados"] = value;
        }

    }

}