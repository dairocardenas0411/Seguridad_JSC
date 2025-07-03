using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class Perfil : System.Web.UI.Page
    {
        private const string CARPETA_IMAGENES = "~/Vista/Recursos/";
        private readonly string[] EXTENSIONES_PERMITIDAS = { ".jpg", ".jpeg", ".png" };
        private const int TAMAÑO_MAXIMO_IMAGEN = 5 * 1024 * 1024; // 5MB

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.Items["datosPerfil"] != null)
                {
                    DataTable datos = Context.Items["datosPerfil"] as DataTable;
                    CargarDatosEnFormulario(datos);
                }
            }
        }

        private void CargarDatosEnFormulario(DataTable datos)
        {
            if (datos != null && datos.Rows.Count > 0)
            {
                DataRow fila = datos.Rows[0];

                hfIdUsuario.Value = fila["idUsuario"]?.ToString() ?? string.Empty;
                txtNombre.Text = fila["nombreUsuario"]?.ToString() ?? string.Empty;
                txtDocumento.Text = fila["documento"]?.ToString() ?? string.Empty;
                txtCelular.Text = fila["celular"]?.ToString() ?? string.Empty;
                txtEmail.Text = fila["email"]?.ToString() ?? string.Empty;
                lblEstado.Text = fila["estado"]?.ToString() ?? string.Empty;

                string foto = fila["foto"]?.ToString();
                if (!string.IsNullOrEmpty(foto))
                {
                    imgFoto.ImageUrl = CARPETA_IMAGENES + foto;
                    hfNombreImagen.Value = foto;
                }
            }
        }


        private string GuardarImagenPrincipal(FileUpload fileUpload, bool esPrincipal)
        {
            // Validar que se haya seleccionado un archivo
            if (!fileUpload.HasFile)
            {
                MostrarError("Debe seleccionar una imagen.");
                return string.Empty;
            }

            // Validar tamaño del archivo
            if (fileUpload.PostedFile.ContentLength > TAMAÑO_MAXIMO_IMAGEN)
            {
                MostrarError("La imagen no puede superar los 5MB.");
                return string.Empty;
            }

            // Validar extensión del archivo
            string extension = Path.GetExtension(fileUpload.FileName).ToLower();
            if (!EXTENSIONES_PERMITIDAS.Contains(extension))
            {
                MostrarError("El formato de la imagen debe ser .jpg, .jpeg o .png.");
                return string.Empty;
            }

            try
            {
                // Generar nombre único para el archivo
                string nombreArchivo = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                string prefijo = esPrincipal ? "principal_" : "adicional_";
                string nuevoNombreArchivo = $"{prefijo}{SanitizarNombreArchivo(nombreArchivo)}_{Guid.NewGuid()}{extension}";
                string rutaCompleta = Server.MapPath(CARPETA_IMAGENES) + nuevoNombreArchivo;

                // Crear directorio si no existe
                string directorio = Path.GetDirectoryName(rutaCompleta);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                // Guardar archivo
                fileUpload.SaveAs(rutaCompleta);
                return nuevoNombreArchivo;
            }
            catch (Exception ex)
            {
                MostrarError($"Error al guardar la imagen: {ex.Message}");
                return string.Empty;
            }
        }

        private string SanitizarNombreArchivo(string nombreArchivo)
        {
            // Remover caracteres especiales del nombre del archivo
            return Regex.Replace(nombreArchivo, @"[^a-zA-Z0-9_-]", "");
        }

        private void MostrarError(string mensaje)
        {
            string script = $"Swal.fire({{ title: 'Error', text: '{mensaje}', icon: 'error', confirmButtonText: 'OK' }});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", script, true);
        }




        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }


        private void MostrarExito(string mensaje, string urlRedireccion)
        {
            string script = $@"
        <script>
            Swal.fire({{
                icon: 'success',
                title: '¡Éxito!',
                text: '{mensaje}',
                confirmButtonText: 'OK'
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    window.location.href = '{urlRedireccion}';
                }}
            }});
        </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, false);
        }
        protected void bntActualizar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (!int.TryParse(hfIdUsuario.Value, out int idUsuario) || idUsuario <= 0)
                    throw new Exception("El ID del Usuario no es válido.");

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("El nombre del usuario es obligatorio.");

                if (txtNombre.Text.Trim().Length < 2)
                    throw new Exception("El nombre debe tener al menos 2 caracteres.");

                if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                    !int.TryParse(txtDocumento.Text.Trim(), out int documento) || documento <= 0)
                    throw new Exception("Debe ingresar un número de documento válido.");

                if (!ValidarEmail(txtEmail.Text))
                    throw new Exception("Debe ingresar un email válido.");

                if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                    throw new Exception("La contraseña es obligatoria.");

                if (TxtPassword.Text.Trim().Length < 6)
                    throw new Exception("La contraseña debe tener al menos 6 caracteres.");

                string imagenUsuario;
                if (txtImagenPrincipal.HasFile)
                {
                    imagenUsuario = GuardarImagenPrincipal(txtImagenPrincipal, true);
                    if (string.IsNullOrEmpty(imagenUsuario))
                        return;
                }
                else
                {
                    imagenUsuario = hfNombreImagen.Value;
                }

                ClUsuarioE usuario = new ClUsuarioE
                {
                    IdUsuario = idUsuario,
                    nombreUsuario = txtNombre.Text.Trim(),
                    documento = documento,
                    celular = txtCelular.Text.Trim(),
                    email = txtEmail.Text.Trim().ToLower(),
                    contraseña = TxtPassword.Text.Trim(),
                    foto = imagenUsuario
                };

                ClUsuarioL logica = new ClUsuarioL();
                bool resultado = logica.MtdActualizarUsuario(usuario);
                if (resultado)
                {
                    // ✅ Solo actualiza la sesión si el usuario editado es el mismo que el de la sesión activa
                    if (Session["idUsuario"] != null && Convert.ToInt32(Session["idUsuario"]) == usuario.IdUsuario)
                    {
                        Session["nombreUsuario"] = usuario.nombreUsuario;
                        Session["foto"] = usuario.foto;
                        Session["email"] = usuario.email;

                        ((Master)this.Master).ActualizarDatosUsuario();
                    }

                    string url = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "default.aspx";
                    MostrarExito("Usuario actualizado correctamente", url);
                }


                else
                {
                    throw new Exception("No se pudo actualizar el usuario. Verifique los datos e intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }


    }
}