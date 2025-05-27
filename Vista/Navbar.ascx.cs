using Seguridad_JSC.Entidades;
using Seguridad_JSC.Logica;
using System;
using System.Web.UI;

namespace Seguridad_JSC.Vista
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        private readonly ClLoginL usuarioLogica = new ClLoginL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null && Session["idRol"] != null)
                {
                    if (int.TryParse(Session["idRol"].ToString(), out int idRol))
                    {
                        RedirectUser(idRol);
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MostrarAlerta("warning", "Campos requeridos", "Por favor, ingrese el correo electrónico y la contraseña.");
                    return;
                }

                if (!IsValidEmail(txtUsuario.Text.Trim()))
                {
                    MostrarAlerta("warning", "Formato incorrecto", "Por favor, ingrese un email válido.");
                    return;
                }

                ClLoginE usuario = new ClLoginE
                {
                    email = txtUsuario.Text.Trim(),
                    password = txtPassword.Text.Trim()
                };

                var usuarioAutenticado = usuarioLogica.MtdIngresoL(usuario);

                if (usuarioAutenticado != null)
                {
                    Session["idUsuario"] = usuarioAutenticado.idUsuario;
                    Session["email"] = usuarioAutenticado.email;
                    Session["nombreUsuario"] = usuarioAutenticado.nombreUsuario;
                    Session["idRol"] = usuarioAutenticado.idRol;

                    MostrarAlertaConRedireccion("success", "¡Bienvenido!", "Inicio de sesión exitoso.", usuarioAutenticado.idRol);
                }
                else
                {
                    MostrarAlerta("error", "Credenciales incorrectas", "Email o contraseña incorrectos. Por favor, verifique sus datos.");
                    // Limpiar los campos
                    txtUsuario.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error del sistema", "Ha ocurrido un error inesperado. Por favor, inténtelo nuevamente.");
                // Log del error (opcional)
                System.Diagnostics.Debug.WriteLine($"Error en login: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        private void RedirectUser(int idRol)
        {
            string url;
            switch (idRol)
            {
                case 1:
                    url = ResolveUrl("~/Vista/admin.aspx");
                    break;
                case 2:
                    url = ResolveUrl("~/Vista/TecnicoSeguridad_JSC.aspx");
                    break;
                default:
                    MostrarAlerta("error", "Rol inválido", "No tienes un rol válido asignado.");
                    return;
            }
            Response.Redirect(url, false);
        }

        // Método principal para mostrar alertas que mantiene el modal abierto
        private void MostrarAlerta(string icono, string titulo, string mensaje)
        {
            string script = $@"
                setTimeout(function() {{
                    Swal.fire({{
                        icon: '{icono}',
                        title: '{titulo}',
                        text: '{mensaje}',
                        confirmButtonText: 'Aceptar',
                        allowOutsideClick: false,
                        backdrop: true
                    }}).then((result) => {{
                        if (result.isConfirmed || result.isDismissed) {{
                            // Mantener el modal abierto después de cerrar la alerta
                            setTimeout(function() {{
                                $('#loginModal').modal('show');
                            }}, 150);
                        }}
                    }});
                }}, 100);";

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alertaSweetAlert_" + DateTime.Now.Ticks, script, true);
        }

        // Método para mostrar alerta con redirección (para login exitoso)
        private void MostrarAlertaConRedireccion(string icono, string titulo, string mensaje, int idRol)
        {
            string url = "";
            switch (idRol)
            {
                case 1:
                    url = ResolveUrl("~/Vista/admin.aspx");
                    break;
                case 2:
                    url = ResolveUrl("~/Vista/TecnicoSeguridad_JSC.aspx");
                    break;
            }

            string script = $@"
                setTimeout(function() {{
                    Swal.fire({{
                        icon: '{icono}',
                        title: '{titulo}',
                        text: '{mensaje}',
                        confirmButtonText: 'Continuar',
                        timer: 3000,
                        timerProgressBar: true,
                        allowOutsideClick: false
                    }}).then((result) => {{
                        window.location.href = '{url}';
                    }});
                }}, 100);";

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alertaExito_" + DateTime.Now.Ticks, script, true);
        }

        // Método alternativo para mensajes simples sin modal
        private void MostrarMensaje(string mensaje)
        {
            string script = $@"
                setTimeout(function() {{
                    Swal.fire({{
                        icon: 'info',
                        title: 'Aviso',
                        text: '{mensaje}',
                        confirmButtonText: 'Aceptar'
                    }});
                }}, 100);";

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "mensajeGenerico_" + DateTime.Now.Ticks, script, true);
        }
    }
}