using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class Cotizacion : System.Web.UI.Page
    {
        private readonly ClCotizacionL _CotizacionL = new ClCotizacionL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
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
            } catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al cargar las razas: {ex.Message}');", true);
            }

        }

        protected void BtnRegistrarCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreCliente = TxtNombreCliente.Text.Trim();
                string apellidoCliente = TxtApellidoCliente.Text.Trim();
                string telefono = TxtTelefono.Text.Trim();
                string documentoTexto = txtDocumento.Text.Trim();
                string email = txtEmail.Text.Trim();
                string direccionInstalacion = txtDireccion.Text.Trim();
                string cantidadTexto = TxtCantidad.Text.Trim();
                string observaciones = TxtObservaciones.Text.Trim();

                // Validaciones de campos obligatorios
                if (string.IsNullOrWhiteSpace(nombreCliente) ||
                    string.IsNullOrWhiteSpace(apellidoCliente) ||
                    string.IsNullOrWhiteSpace(telefono) ||
                    string.IsNullOrWhiteSpace(documentoTexto) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(direccionInstalacion) ||
                    string.IsNullOrWhiteSpace(cantidadTexto))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "Swal.fire('Campos requeridos', 'Todos los campos son obligatorios.', 'warning');", true);
                    return;
                }

                // Validación de número de documento
                if (!int.TryParse(documentoTexto, out int documento))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "Swal.fire('Dato inválido', 'El documento debe ser un número válido.', 'warning');", true);
                    return;
                }

                // Validación de cantidad
                if (!int.TryParse(cantidadTexto, out int cantidad) || cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "Swal.fire('Dato inválido', 'La cantidad debe ser un número mayor que cero.', 'warning');", true);
                    return;
                }

                // Validación del producto
                int idProducto = Convert.ToInt32(ddlListaProductos.SelectedValue);
                if (idProducto == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "Swal.fire('Error', 'Debe seleccionar un producto.', 'warning');", true);
                    return;
                }

                // Si todo está correcto, crea el objeto y lo registra
                ClCotizacionE oCotizacion = new ClCotizacionE
                {
                    nombreCliente = nombreCliente,
                    apellidoCliente = apellidoCliente,
                    documento = documento,
                    telefono = telefono,
                    email = email,
                    direccionInstalacion = direccionInstalacion,
                    idProducto = idProducto,
                    cantidad = cantidad,
                    observaciones = observaciones
                };

                _CotizacionL.MtdRegistroCotizacion(oCotizacion);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "Swal.fire('Registro exitoso', 'La cotización se ha registrado correctamente.', 'success');", true);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }


        private void LimpiarCampos()
        {
            if(ddlListaProductos.SelectedIndex == 0)
            {
                TxtNombreCliente.Text = string.Empty;
                TxtApellidoCliente.Text = string.Empty;
                TxtTelefono.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtDocumento.Text = string.Empty;
                txtDireccion.Text = string.Empty;
                TxtCantidad.Text = string.Empty;
                TxtObservaciones.Text = string.Empty;
                ddlListaProductos.SelectedIndex = 0;

            }
            else
            {
                TxtNombreCliente.Text = string.Empty;
                TxtApellidoCliente.Text = string.Empty;
                TxtTelefono.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtDocumento.Text = string.Empty;
                txtDireccion.Text = string.Empty;
                TxtCantidad.Text = string.Empty;
                TxtObservaciones.Text = string.Empty;
                ddlListaProductos.SelectedIndex = 0;
            }
        }
    }
}