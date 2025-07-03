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
    public partial class Categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SOLUCIÓN INMEDIATA: Deshabilitar validación no intrusiva
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación manual en servidor
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    MostrarMensaje("Por favor, ingrese el nombre de la categoría.", "error");
                    return;
                }

                ClCategoriaE oCategoria = new ClCategoriaE
                {
                    nombreCategoria = txtNombreCategoria.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim()
                };

                ClCategoriaL LogicaCategoria = new ClCategoriaL();
                bool resultado = LogicaCategoria.MtdRegistroCategoria(oCategoria);

                if (resultado)
                {
                    LimpiarCampos();
                    MostrarMensaje("La categoría se guardó correctamente.", "success");
                    CargarCategorias();
                    // Cerrar modal después de guardar exitosamente
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModal",
                        "$('#modalCategoria').modal('hide');", true);
                }
                else
                {
                    MostrarMensaje("Hubo un problema al guardar la categoría.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Ha ocurrido un error: {ex.Message}", "error");
            }
        }

        protected void CargarCategorias()
        {
            try
            {
                ClCategoriaL LogicaCategoria = new ClCategoriaL();
                List<ClCategoriaE> listaCategorias = LogicaCategoria.MtdListaCategorias();

                if (listaCategorias != null)
                {
                    gvCategorias.DataSource = listaCategorias;
                    gvCategorias.DataBind();
                }
                else
                {
                    gvCategorias.DataSource = new List<ClCategoriaE>();
                    gvCategorias.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Ha ocurrido un error al cargar las categorías: {ex.Message}", "error");
            }
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvCategorias.EditIndex = e.NewEditIndex;
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al editar: {ex.Message}", "error");
            }
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvCategorias.EditIndex = -1;
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cancelar edición: {ex.Message}", "error");
            }
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvCategorias.Rows[e.RowIndex];

                // Obtener valores de los controles de edición
                TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
                TextBox txtDesc = (TextBox)row.FindControl("txtEditDescripcion");

                if (txtNombre == null || txtDesc == null)
                {
                    MostrarMensaje("Error al obtener los controles de edición.", "error");
                    return;
                }

                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDesc.Text.Trim();

                // Validación manual
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MostrarMensaje("El nombre de la categoría es requerido.", "error");
                    return;
                }

                ClCategoriaE oCategoria = new ClCategoriaE
                {
                    idCategoria = idCategoria,
                    nombreCategoria = nombre,
                    descripcion = descripcion
                };

                ClCategoriaL logica = new ClCategoriaL();
                bool actualizado = logica.MtdActualizarCategoria(oCategoria);

                gvCategorias.EditIndex = -1;

                if (actualizado)
                {
                    MostrarMensaje("La categoría fue actualizada correctamente.", "success");
                }
                else
                {
                    MostrarMensaje("No se pudo actualizar la categoría.", "error");
                }

                CargarCategorias();
            }
            catch (Exception ex)
            {
                gvCategorias.EditIndex = -1;
                MostrarMensaje($"Error al actualizar: {ex.Message}", "error");
                CargarCategorias();
            }
        }

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
                ClCategoriaL logica = new ClCategoriaL();
                bool eliminado = logica.MtdEliminarCategoria(idCategoria);

                if (eliminado)
                {
                    MostrarMensaje("La categoría fue eliminada correctamente.", "success");
                }
                else
                {
                    MostrarMensaje("No se pudo eliminar la categoría.", "error");
                }

                CargarCategorias();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "error");
            }
        }

        // Métodos auxiliares
        private void LimpiarCampos()
        {
            txtNombreCategoria.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            string icon = tipo == "success" ? "success" : "error";
            string title = tipo == "success" ? "¡Éxito!" : "Error";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert",
                $"Swal.fire('{title}', '{mensaje}', '{icon}');", true);
        }

        // Evento para limpiar campos al abrir modal
        protected void btnAbrirModal_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}