using Seguridad_JSC.Logica;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class ListaProductosProveedor : System.Web.UI.Page
    {
        private ClProveedorL logicaProveedor = new ClProveedorL();

        private int CurrentPage
        {
            get => ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0;
            set => ViewState["CurrentPage"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idProveedorSeleccionado"] != null)
                {
                    int idProveedor = Convert.ToInt32(Session["idProveedorSeleccionado"]);
                    CargarProductosProveedor(idProveedor);
                }
                else
                {
                    Response.Redirect("ListaProveedores.aspx");
                }
            }
        }

        private void CargarProductosProveedor(int idProveedor)
        {
            DataTable productos = logicaProveedor.MtdDatosProductoProveedor(idProveedor);

            if (productos != null && productos.Rows.Count > 0)
            {
                lblSinProductos.Visible = false;

                int filasPorPagina = 10;
                if (int.TryParse(TxtNumeroFilas.Text, out int filasUsuario) && filasUsuario > 0)
                {
                    filasPorPagina = filasUsuario;
                }

                int totalFilas = productos.Rows.Count;
                int totalPaginas = (int)Math.Ceiling((double)totalFilas / filasPorPagina);

                if (CurrentPage >= totalPaginas) CurrentPage = totalPaginas - 1;
                if (CurrentPage < 0) CurrentPage = 0;

                DataTable tablaPagina = productos.Clone();
                int inicio = CurrentPage * filasPorPagina;
                for (int i = inicio; i < inicio + filasPorPagina && i < totalFilas; i++)
                {
                    tablaPagina.ImportRow(productos.Rows[i]);
                }

                rptProductos.DataSource = tablaPagina;
                rptProductos.DataBind();

                lblPageInfo.Text = $"Página {CurrentPage + 1} de {totalPaginas}";
                btnPrev.Enabled = CurrentPage > 0;
                btnNext.Enabled = CurrentPage < totalPaginas - 1;
            }
            else
            {
                rptProductos.DataSource = null;
                rptProductos.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertNoDatos",
                        "Swal.fire('Sin datos', 'No se encontraron Productos Asociados a este Proveedor.', 'info');", true);

                lblPageInfo.Text = "";
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            if (Session["idProveedorSeleccionado"] != null)
            {
                int idProveedor = Convert.ToInt32(Session["idProveedorSeleccionado"]);
                CargarProductosProveedor(idProveedor);
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            int idProveedor = Convert.ToInt32(Session["idProveedorSeleccionado"]);
            CargarProductosProveedor(idProveedor);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            int idProveedor = Convert.ToInt32(Session["idProveedorSeleccionado"]);
            CargarProductosProveedor(idProveedor);
        }
    }
}
