using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguridad_JSC.Vista
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        private const string carpetaImagenes = "~/Vista/Recursos/";

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
                CargarRoles();
            }
        }


        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtCelular.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    !txtImagenPrincipal.HasFile ||
                    string.IsNullOrWhiteSpace(ddlRol.SelectedValue))


                {
                    MostrarSweetAlert("Advertencia", "Por favor, complete todos los campos.", "warning");
                    return;
                }

                ClUsuarioL logicaRegistro = new ClUsuarioL();
                bool documentoExiste = logicaRegistro.MtdVerificarDocumento(int.Parse(txtDocumento.Text));
                bool correoExiste = logicaRegistro.MtdVerificarCorreo(txtEmail.Text);

                if (documentoExiste)
                {
                    MostrarSweetAlert("Advertencia", "El documento ya está registrado.", "warning");
                    return;
                }

                if (correoExiste)
                {
                    MostrarSweetAlert("Advertencia", "El correo ya está registrado.", "warning");
                    return;
                }
                string imagenUsuario = GuardarImagenPrincipal(txtImagenPrincipal, true);

                ClUsuarioE oRegistro = new ClUsuarioE
                {
                    documento = int.Parse(txtDocumento.Text),
                    nombreUsuario = txtNombreUsuario.Text,
                    celular = txtCelular.Text,
                    email = txtEmail.Text,
                    contraseña = txtPassword.Text,
                    foto = imagenUsuario,
                    idRol = Convert.ToInt32(ddlRol.SelectedValue)
                };

                bool resultado = logicaRegistro.MtdRegistroUsuario(oRegistro);

                if (resultado)
                {
                    EnviarCorreoConfirmacion(oRegistro.email, oRegistro.nombreUsuario);
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
        private void CargarRoles()
        {
            try
            {
                ClRolL oListaRolL = new ClRolL();
                List<ClRolE> listaRol = oListaRolL.MtdListarRoles();

                if (listaRol.Count > 0)
                {
                    ddlRol.DataSource = listaRol;
                    ddlRol.DataTextField = "nombreRol";
                    ddlRol.DataValueField = "idRol";
                    ddlRol.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertNoProductos",
                       "Swal.fire('Sin datos', 'No se encontraron Productos disponibles.', 'info');", true);
                }
                ddlRol.Items.Insert(0, new ListItem("--Seleccione un Rol--", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al cargar los Productos: {ex.Message}');", true);
            }

        }
        private string GuardarImagenPrincipal(FileUpload fileUpload, bool esPrincipal)
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
        protected void EnviarCorreoConfirmacion(string email, string nombreUsuario)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUser = "dairo.gustavo10@gmail.com";
                string smtpPassword = "jnyw ygky voax baiu";

                ClEnvioEmailL.EmailService emailService = new ClEnvioEmailL.EmailService(
                    smtpServer, smtpPort, smtpUser, smtpPassword);

                ClEnvioEmailE.Email emailMessage = new ClEnvioEmailE.Email
                {
                    To = email,
                    Subject = "¡Felicidades! Te has registrado exitosamente en Seguridad JSC S.A.S",
                    Body = $@"
                            <h2 style='color:#4CAF50; text-align:center;'>Seguridad JSC S.A.S</h2>
                            <div style='text-align:center; margin-bottom:20px;'>
                            <img src='https://scontent.fbog20-1.fna.fbcdn.net/v/t39.30808-6/362956246_809904134191119_6749775445275313641_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=FOkyKxdXF4sQ7kNvwHwNBF1&_nc_oc=AdkbM_Q_0g_8c2ja8RPOrly5UbP52LEqqalI_m7SDvY9Znj9bmVNoa7HlFBP0eEck_0&_nc_zt=23&_nc_ht=scontent.fbog20-1.fna&_nc_gid=5VvxeiQs3HNcCctow0OW1A&oh=00_AfP0DWjf5c3W7Y1aTOwpQSOgYUbBTA1fVXhhINBbK0SQ9A&oe=68580891' 
                            alt='Logo AgroControl' style='width:150px; height:auto; border-radius:20px;'>
                            </div>
                            <p style='font-size:16px; line-height:1.6;'>
                            <strong>Bienvenido a Seguridad JCS S.A.S Estimad@, {nombreUsuario}!<br> 
                            Gracias por registrarte en Seguridad JCS S.A.S . Tu cuenta ha sido creada exitosamente y ahora puedes acceder a todas nuestras funcionalidades.
                            </p>
                            <p style='font-size:16px; line-height:1.6;'>
                            
                            </p>
                            <p style='font-size:16px; line-height:1.6; text-align:justify;'>
                            En Seguridad JCS S.A.S , trabajamos para ofrecerte las mejores herramientas de gestión para tus actividades. Nos comprometemos con el desarrollo 
                            sostenible y la innovación en el sector.
                            </p>
                            <p style='font-size:16px; line-height:1.6;'>
                            <p style='font-size:16px; line-height:1.6;'>
                            Saludos,<br>El equipo de Seguridad JCS S.A.S 
                            </p>",
                    IsHtml = true
                };

                bool correoEnviado = emailService.SendEmail(emailMessage);

                string alerta = correoEnviado ?
                    @"
                        Swal.fire({
                            icon: 'success',
                            title: 'Éxito',
                            text: 'El correo se envió correctamente'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                mostrarAlertaRegistroFinca();
                            }
                        });
                    " :
                    @"
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'El correo no se pudo enviar. Inténtalo nuevamente.'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                mostrarAlertaRegistroFinca();
                            }
                        });
                    ";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", alerta, true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", @"
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'El correo no se pudo enviar. Inténtalo nuevamente.'
                }).then((result) => {
                    if (result.isConfirmed) {
                        mostrarAlertaRegistroFinca();
                    }
                });
            ", true);
            }
        }


    }
}