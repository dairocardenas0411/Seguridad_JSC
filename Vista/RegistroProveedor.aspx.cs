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
	public partial class RegistroProveedor : System.Web.UI.Page
	{
        private const string carpetaImagenes = "~/Vista/Recursos/";
        protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreProveedor.Text) ||
                    string.IsNullOrWhiteSpace(txtCelular.Text) ||
                    string.IsNullOrWhiteSpace(txtEmpresa.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    !txtImagenProveedor.HasFile )


                {
                    MostrarSweetAlert("Advertencia", "Por favor, complete todos los campos.", "warning");
                    return;
                }

                
                string imagenProveedor = GuardarImagenProveedor(txtImagenProveedor, true);

                ClProveedorE oRegistro = new ClProveedorE
                {
                    documento = int.Parse(txtDocumento.Text),
                    nombre = txtNombreProveedor.Text,
                    celular = txtCelular.Text,
                    empresa = txtEmpresa.Text,
                    email = txtEmail.Text,
                    imagen = imagenProveedor
                };

                ClProveedorL logicaProveedor = new ClProveedorL();
                bool resultado = logicaProveedor.MtdRegistroProveedor(oRegistro);

                if (resultado)
                {
                    MostrarSweetAlert("Éxito", "Usuario registrado correctamente.", "success");
                }
                else
                {
                    MostrarSweetAlert("Error", "Ocurrió un error al registrar el usuario.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarSweetAlert("Error", $"Se produjo un error: {ex.Message}", "error");
            }
        }

        private void MostrarSweetAlert(string titulo, string mensaje, string tipo, string urlRedireccion = null)
        {
            string script = $@"
    Swal.fire({{
        title: '{HttpUtility.JavaScriptStringEncode(titulo)}',
        text: '{HttpUtility.JavaScriptStringEncode(mensaje)}',
        icon: '{tipo}',
        confirmButtonText: 'Aceptar'
    }}).then((result) => {{
        if (result.isConfirmed) {{
            {(string.IsNullOrEmpty(urlRedireccion) ? "" : $"window.location.href = '{urlRedireccion}';")}
        }}
    }});
    ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
        }
        private string GuardarImagenProveedor(FileUpload fileUpload, bool esPrincipal)
        {
            if (fileUpload.HasFile)
            {
                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    string nombreArchivo = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                    string prefijo = esPrincipal ? "principal_" : "adicional_";
                    string nuevoNombreArchivo = $"{prefijo}{nombreArchivo}_{Guid.NewGuid()}{extension}";
                    string ruta = Server.MapPath(carpetaImagenes) + nuevoNombreArchivo;

                    try
                    {
                        string directorio = Path.GetDirectoryName(ruta);
                        if (!Directory.Exists(directorio))
                        {
                            Directory.CreateDirectory(directorio);
                        }

                        fileUpload.SaveAs(ruta);
                        return nuevoNombreArchivo;
                    }
                    catch (Exception ex)
                    {
                        string script = $"Swal.fire({{ title: 'Error', text: 'Error al guardar la imagen: {ex.Message}', icon: 'error', confirmButtonText: 'OK' }});";
                        ClientScript.RegisterStartupScript(this.GetType(), "alertError", script, true);
                        return string.Empty;
                    }
                }
                else
                {
                    string script = "Swal.fire({ title: 'Error', text: 'El formato de la imagen debe ser .jpg o .png.', icon: 'error', confirmButtonText: 'OK' });";
                    ClientScript.RegisterStartupScript(this.GetType(), "alertError", script, true);
                    return string.Empty;
                }
            }
            else
            {
                string script = "Swal.fire({ title: 'Error', text: 'Debe seleccionar una imagen.', icon: 'error', confirmButtonText: 'OK' });";
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", script, true);
                return string.Empty;
            }
        }
    }
}