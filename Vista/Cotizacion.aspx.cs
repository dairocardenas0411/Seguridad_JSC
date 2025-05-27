using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class Cotizacion : System.Web.UI.Page
    {
        private readonly ClCotizacionL _CotizacionL = new ClCotizacionL();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarGridProductos();
            }
        }

        private void CargarProductos()
        {
            try
            {
                ClListaProductoL logicaProducto = new ClListaProductoL();
                List<ClProductoE> lista = logicaProducto.MtdListarProducto();

                ddlListaProductos.DataSource = lista;
                ddlListaProductos.DataTextField = "nombreProducto";
                ddlListaProductos.DataValueField = "idProducto";
                ddlListaProductos.DataBind();
                ddlListaProductos.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "No se pudieron cargar los productos: " + ex.Message, "error");
            }
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

            var nuevo = new ProductoCotizacionTemp
            {
                IdProducto = idProducto,
                NombreProducto = nombreProducto,
                Cantidad = cantidad
            };

            ProductosSeleccionados.Add(nuevo);
            CargarGridProductos();
            TxtCantidad.Text = "1";
            ddlListaProductos.SelectedIndex = 0;
        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var lista = ProductosSeleccionados;
                if (index >= 0 && index < lista.Count)
                {
                    lista.RemoveAt(index);
                    ProductosSeleccionados = lista;
                    CargarGridProductos();
                }
            }
        }

        private void CargarGridProductos()
        {
            GridViewProductos.DataSource = ProductosSeleccionados;
            GridViewProductos.DataBind();
        }

        protected void BtnRegistrarCotizacion_Click(object sender, EventArgs e)
        {
            // Validar que haya productos
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
            {
                MostrarAlerta("Advertencia", "Debe agregar al menos un producto para cotizar.", "warning");
                return;
            }

            // Validar campos obligatorios del cliente
            if (string.IsNullOrWhiteSpace(TxtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(TxtApellidoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MostrarAlerta("Advertencia", "Por favor complete todos los campos obligatorios.", "warning");
                return;
            }

            try
            {
                // Crear objeto de cotización
                ClCotizacionE cotizacion = new ClCotizacionE
                {
                    nombreCliente = TxtNombreCliente.Text.Trim(),
                    apellidoCliente = TxtApellidoCliente.Text.Trim(),
                    documento = int.Parse(txtDocumento.Text.Trim()),
                    email = txtEmail.Text.Trim(),
                    telefono = TxtTelefono.Text.Trim(),
                    direccionInstalacion = txtDireccion.Text.Trim(),
                    tipoTrabajo = "Cotizacion",
                    observaciones = TxtObservaciones.Text.Trim(),
                    fechaCotizacion = DateTime.Now,
                };

                // ✅ Asegurarse que los productos sean del tipo correcto
                cotizacion.Productos = ProductosSeleccionados.Select(p => new ProductoCotizacion
                {
                    idProducto = p.IdProducto,
                    cantidad = p.Cantidad,
                   
                }).ToList();

                // Enviar a lógica de negocio
                int resultado = _CotizacionL.MtdRegistroCotizacion(cotizacion);

                // Validar respuesta
                if (resultado > 0)
                {
                    MostrarAlerta("Éxito", "Cotización registrada correctamente.", "success");
                    LimpiarFormulario();
                }
                else
                {
                    MostrarAlerta("Error", "No se pudo registrar la cotización.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Ocurrió un error: " + ex.Message, "error");
            }
        }


        private void MostrarAlerta(string titulo, string mensaje, string icono)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta",
                $"Swal.fire('{titulo}', '{mensaje}', '{icono}');", true);
        }

        private void LimpiarFormulario()
        {
            TxtNombreCliente.Text = "";
            TxtApellidoCliente.Text = "";
            txtDocumento.Text = "";
            txtEmail.Text = "";
            TxtTelefono.Text = "";
            txtDireccion.Text = "";
            TxtObservaciones.Text = "";
            ProductosSeleccionados = new List<ProductoCotizacionTemp>();
            CargarGridProductos();
        }
    }
    [Serializable]
    public class ProductoCotizacionTemp
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
    }

    
}
