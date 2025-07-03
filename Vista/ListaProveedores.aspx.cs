using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class ListaProveedores : System.Web.UI.Page
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
                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            try
            {
                var proveedores = logicaProveedor.MtdListaProveedores();
                if (proveedores != null && proveedores.Count > 0)
                {
                    int pageSize = 8;
                    if (!string.IsNullOrWhiteSpace(TxtNumeroColm.Text) && int.TryParse(TxtNumeroColm.Text, out int resultado))
                    {
                        if (resultado > 0)
                            pageSize = resultado;
                    }

                    PagedDataSource pgitems = new PagedDataSource
                    {
                        DataSource = proveedores,
                        AllowPaging = true,
                        PageSize = pageSize,
                        CurrentPageIndex = CurrentPage
                    };

                    rptProveedores.DataSource = pgitems;
                    rptProveedores.DataBind();

                    btnPrev.Enabled = !pgitems.IsFirstPage;
                    btnNext.Enabled = !pgitems.IsLastPage;
                    lblPageInfo.Text = $"Página {CurrentPage + 1} de {pgitems.PageCount}";
                }
                else
                {
                    rptProveedores.DataSource = null;
                    rptProveedores.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoDatos",
                        "Swal.fire('Sin datos', 'No se encontraron Proveedores disponibles.', 'info');", true);

                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    lblPageInfo.Text = string.Empty;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    "Swal.fire('Error', 'Ocurrió un error al cargar los proveedores.', 'error');", true);
            }
        }

        protected void btnAplicarNumero_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarProveedores();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                CargarProveedores();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            CargarProveedores();
        }

        protected void btnProductos_Command(object sender, CommandEventArgs e)
        {
            int idProveedor = Convert.ToInt32(e.CommandArgument);
            Session["idProveedorSeleccionado"] = idProveedor;
            Response.Redirect("ListaProductosProveedor.aspx");
        }

        protected void btnGuardarProveedor_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int idProveedor = Convert.ToInt32(e.CommandArgument);
                Button btnGuardar = (Button)sender;
                RepeaterItem item = (RepeaterItem)btnGuardar.NamingContainer;

                TextBox txtNombre = (TextBox)item.FindControl("txtNombre");
                TextBox txtDocumento = (TextBox)item.FindControl("txtDocumento");
                TextBox txtEmpresa = (TextBox)item.FindControl("txtEmpresa");
                TextBox txtEmail = (TextBox)item.FindControl("txtEmail");
                TextBox txtCelular = (TextBox)item.FindControl("txtCelular");
                FileUpload fuImagen = (FileUpload)item.FindControl("fuImagen");
                HiddenField hfImagenActual = (HiddenField)item.FindControl("hfImagenActual");

                string nombreImagen = hfImagenActual.Value;
                string rutaRelativa = "~/Vista/Recursos/";
                string rutaFisica = Server.MapPath(rutaRelativa);

                if (fuImagen.HasFile)
                {
                    string extension = Path.GetExtension(fuImagen.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                    {
                        // Eliminar la imagen anterior si existe
                        string imagenAnterior = hfImagenActual.Value;
                        if (!string.IsNullOrEmpty(imagenAnterior))
                        {
                            string rutaAnterior = Path.Combine(rutaFisica, imagenAnterior);
                            if (File.Exists(rutaAnterior))
                            {
                                File.Delete(rutaAnterior);
                            }
                        }

                        // Guardar nueva imagen
                        nombreImagen = $"img_proveedor_{idProveedor}_{DateTime.Now.Ticks}{extension}";
                        string rutaCompleta = Path.Combine(rutaFisica, nombreImagen);
                        fuImagen.SaveAs(rutaCompleta);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "errorFormato",
                            "Swal.fire('Formato inválido', 'Solo se permiten imágenes JPG, PNG, JPEG o GIF.', 'warning');", true);
                        return;
                    }
                }

                int.TryParse(txtDocumento.Text.Trim(), out int documento);

                ClProveedorE proveedor = new ClProveedorE
                {
                    idProveedor = idProveedor,
                    nombre = txtNombre.Text.Trim(),
                    documento = documento,
                    empresa = txtEmpresa.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    celular = txtCelular.Text.Trim(),
                    imagen = nombreImagen
                };

                int result = logicaProveedor.MtdActualizarProveedor(proveedor);

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "actualizado",
                        "Swal.fire('Éxito', 'Proveedor actualizado correctamente.', 'success');", true);
                    CargarProveedores();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "Swal.fire('Error', 'No se pudo actualizar el proveedor.', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "excepcion",
                    $"Swal.fire('Excepción', 'Error al actualizar proveedor: {ex.Message}', 'error');", true);
            }
        }


    }
}
