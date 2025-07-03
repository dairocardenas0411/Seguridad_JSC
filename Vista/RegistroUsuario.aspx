<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="Seguridad_JSC.Vista.RegistroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <style>
        /* Reset y variables CSS */
        :root {
            --primary-color: #667eea;
            --secondary-color: #764ba2;
            --accent-color: #f093fb;
            --success-color: #4ecdc4;
            --danger-color: #ff6b6b;
            --warning-color: #feca57;
            --dark-color: #2c3e50;
            --light-color: #ecf0f1;
            --white: #ffffff;
            --shadow-light: 0 2px 10px rgba(0,0,0,0.1);
            --shadow-medium: 0 8px 30px rgba(0,0,0,0.12);
            --shadow-heavy: 0 15px 35px rgba(0,0,0,0.1);
            --border-radius: 12px;
            --transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
        }

        /* Contenedor principal */
        .registration-wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 15px;
        }

        .form-container {
            background: var(--white);
            border-radius: var(--border-radius);
            box-shadow: var(--shadow-heavy);
            padding: 50px;
            width: 100%;
            max-width: 900px;
            position: relative;
            overflow: hidden;
            backdrop-filter: blur(10px);
            animation: slideInUp 0.6s ease-out;
        }

            .form-container::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 4px;
                background: linear-gradient(90deg, var(--primary-color), var(--accent-color), var(--success-color));
                animation: shimmer 2s ease-in-out infinite;
            }

        @keyframes slideInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes shimmer {
            0%, 100% {
                opacity: 1;
            }

            50% {
                opacity: 0.7;
            }
        }

        /* Header del formulario */
        .form-header {
            text-align: center;
            margin-bottom: 45px;
        }

            .form-header h2 {
                color: var(--dark-color);
                font-size: 1.8rem;
                font-weight: 700;
                margin-bottom: 12px;
                position: relative;
            }

            .form-header .subtitle {
                color: #6c757d;
                font-size: 1.1rem;
                font-weight: 400;
            }

            .form-header .icon {
                display: inline-block;
                width: 80px;
                height: 80px;
                background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
                border-radius: 50%;
                margin-bottom: 25px;
                display: flex;
                align-items: center;
                justify-content: center;
                color: var(--white);
                font-size: 2rem;
                animation: pulse 2s ease-in-out infinite;
            }

        @keyframes pulse {
            0%, 100% {
                transform: scale(1);
            }

            50% {
                transform: scale(1.05);
            }
        }

        /* Layout de dos columnas */
        .form-columns {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 30px;
            margin-bottom: 30px;
        }

        /* Grupos de campos */
        .form-group {
            margin-bottom: 30px;
            position: relative;
            display: flex;
            flex-direction: column;
        }

            .form-group label {
                display: block;
                margin-bottom: 10px;
                color: var(--dark-color);
                font-weight: 600;
                font-size: 1rem;
                transition: var(--transition);
            }

        /* Campos de entrada */
        .form-control, .form-select {
            width: 100%;
            padding: 18px 60px 18px 25px;
            border: 2px solid #e9ecef;
            border-radius: var(--border-radius);
            font-size: 1.1rem;
            transition: var(--transition);
            background: var(--white);
            position: relative;
        }

            .form-control:focus, .form-select:focus {
                outline: none;
                border-color: var(--primary-color);
                box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
                transform: translateY(-2px);
            }

            .form-control:hover, .form-select:hover {
                border-color: var(--primary-color);
                box-shadow: var(--shadow-light);
            }

        /* Iconos en los campos */
        .input-icon {
            position: absolute;
            right: 20px;
            top: 50%;
            transform: translateY(-50%);
            color: #6c757d;
            font-size: 1.3rem;
            transition: var(--transition);
            pointer-events: none;
            z-index: 2;
        }

        .form-group:focus-within .input-icon {
            color: var(--primary-color);
            transform: translateY(-50%) scale(1.1);
        }

        /* Contenedor de input con icono */
        .input-with-icon {
            position: relative;
            display: flex;
            align-items: center;
        }

        /* Contenedor del botón */
        .button-container {
            display: flex;
            justify-content: center;
            margin-top: 30px;
        }

        /* Botón principal */
        .btn-primary {
            padding: 15px 40px;
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            border: none;
            border-radius: var(--border-radius);
            color: var(--white);
            font-size: 1.1rem;
            font-weight: 600;
            cursor: pointer;
            transition: var(--transition);
            position: relative;
            overflow: hidden;
            min-width: 200px;
        }

            .btn-primary::before {
                content: '';
                position: absolute;
                top: 0;
                left: -100%;
                width: 100%;
                height: 100%;
                background: linear-gradient(90deg, transparent, rgba(255,255,255,0.2), transparent);
                transition: left 0.5s;
            }

            .btn-primary:hover::before {
                left: 100%;
            }

            .btn-primary:hover {
                transform: translateY(-2px);
                box-shadow: var(--shadow-medium);
            }

            .btn-primary:active {
                transform: translateY(0);
            }

            /* Efectos de carga */
            .btn-primary.loading {
                position: relative;
                color: transparent;
            }

                .btn-primary.loading::after {
                    content: '';
                    position: absolute;
                    width: 20px;
                    height: 20px;
                    top: 50%;
                    left: 50%;
                    margin-left: -10px;
                    margin-top: -10px;
                    border: 2px solid #ffffff;
                    border-radius: 50%;
                    border-top-color: transparent;
                    animation: spin 1s linear infinite;
                }

        @keyframes spin {
            to {
                transform: rotate(360deg);
            }
        }

        /* Animaciones de entrada para cada campo */
        .form-group {
            animation: fadeInLeft 0.6s ease-out;
            animation-fill-mode: both;
        }

            .form-group:nth-child(1) {
                animation-delay: 0.1s;
            }

            .form-group:nth-child(2) {
                animation-delay: 0.2s;
            }

            .form-group:nth-child(3) {
                animation-delay: 0.3s;
            }

            .form-group:nth-child(4) {
                animation-delay: 0.4s;
            }

            .form-group:nth-child(5) {
                animation-delay: 0.5s;
            }

            .form-group:nth-child(6) {
                animation-delay: 0.6s;
            }

        @keyframes fadeInLeft {
            from {
                opacity: 0;
                transform: translateX(-30px);
            }

            to {
                opacity: 1;
                transform: translateX(0);
            }
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .form-columns {
                grid-template-columns: 1fr;
                gap: 0;
            }

            .form-container {
                padding: 35px 25px;
                margin: 10px;
                border-radius: 8px;
                max-width: 95%;
            }

            .form-header h2 {
                font-size: 2rem;
            }

            .form-control, .form-select {
                padding: 15px 50px 15px 20px;
                font-size: 1rem;
            }

            .btn-primary {
                padding: 15px 30px;
                font-size: 1rem;
                min-width: 180px;
            }

            .input-icon {
                font-size: 1.1rem;
                right: 15px;
            }
        }

        @media (max-width: 480px) {
            .registration-wrapper {
                padding: 10px;
            }

            .form-container {
                padding: 30px 20px;
                max-width: 100%;
            }

            .form-header h2 {
                font-size: 1.8rem;
            }

            .form-header .icon {
                width: 70px;
                height: 70px;
                font-size: 1.8rem;
            }

            .btn-primary {
                min-width: 160px;
            }
        }

        /* Estados de validación */
        .form-control.error {
            border-color: var(--danger-color);
            animation: shake 0.5s ease-in-out;
        }

        .form-control.success {
            border-color: var(--success-color);
        }

        @keyframes shake {
            0%, 100% {
                transform: translateX(0);
            }

            25% {
                transform: translateX(-5px);
            }

            75% {
                transform: translateX(5px);
            }
        }

        /* Mejoras adicionales */
        .form-group.focused label {
            color: var(--primary-color);
            transform: translateY(-2px);
        }

        /* Tooltips */
        .tooltip {
            position: relative;
            display: inline-block;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 200px;
                background-color: var(--dark-color);
                color: var(--white);
                text-align: center;
                border-radius: 6px;
                padding: 8px;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -100px;
                opacity: 0;
                transition: opacity 0.3s;
                font-size: 0.85rem;
            }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }

        .registro-input::file-selector-button {
            background: var(--primary-color);
            color: white;
            border: none;
            padding: 10px 16px;
            border-radius: var(--border-radius);
            margin-right: 15px;
            cursor: pointer;
            transition: var(--transition);
        }

            .registro-input::file-selector-button:hover {
                background: var(--secondary-color);
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LblidUsuario" runat="server" Text="Label" Visible="false"></asp:Label>

    <div class="registration-wrapper">
        <div class="form-container">
            <div class="form-header">
                <div class="icon">
                    <i class="fas fa-user-plus"></i>
                </div>
                <h2>Registrar Usuario</h2>
                <p class="subtitle">Complete la información para crear una nueva cuenta</p>
            </div>

            <div class="form-columns">
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtDocumento" Text="Documento de Identidad" />
                    <div class="input-with-icon">
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ingrese su número de documento" />
                        <i class="fas fa-id-card input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtNombreUsuario" Text="Nombre de Usuario" />
                    <div class="input-with-icon">
                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su nombre de usuario" />
                        <i class="fas fa-user input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtCelular" Text="Número de Celular" />
                    <div class="input-with-icon">
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" placeholder="Ingrese su número de celular" />
                        <i class="fas fa-mobile-alt input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Correo Electrónico" />
                    <div class="input-with-icon">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese su correo electrónico" />
                        <i class="fas fa-envelope input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Contraseña" />
                    <div class="input-with-icon">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese una contraseña segura" />
                        <i class="fas fa-lock input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlRol" Text="Rol del Usuario" />
                    <div class="input-with-icon">
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select"/>
                           
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtImagenPrincipal">Imagen Usuario:</label>
                    <asp:FileUpload ID="txtImagenPrincipal" runat="server" CssClass="registro-input" ClientIDMode="Static" />
                </div>
                <div >
                    <img id="imgVistaPreviaPrincipal" src="#" alt="ImgUser" style="display: none; width: 150px; height: auto; margin: 0 auto;" />
                </div>

              

            </div>

            <div class="button-container">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Usuario" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
            </div>
        </div>
    </div>
      <script type="text/javascript">
          window.onload = function () {
              var fileInput = document.getElementById("txtImagenPrincipal");

              fileInput.addEventListener("change", function () {
                  var file = this.files[0];
                  var reader = new FileReader();

                  reader.onload = function (e) {
                      var img = document.getElementById("imgVistaPreviaPrincipal");
                      img.src = e.target.result;
                      img.style.display = "block";
                  };

                  if (file) {
                      reader.readAsDataURL(file);
                  }
              });
          };
      </script>
    <script>
       
        // Efectos dinámicos con JavaScript
        document.addEventListener('DOMContentLoaded', function () {
            // Agregar efectos de focus a los campos
            const formControls = document.querySelectorAll('.form-control, .form-select');

            formControls.forEach(control => {
                control.addEventListener('focus', function () {
                    this.parentElement.parentElement.classList.add('focused');
                });

                control.addEventListener('blur', function () {
                    this.parentElement.parentElement.classList.remove('focused');
                });
            });

            // Efecto de carga en el botón
            const btnRegistrar = document.querySelector('.btn-primary');
            btnRegistrar.addEventListener('click', function () {
                this.classList.add('loading');
                // Remover la clase después de 2 segundos (ajustar según necesidad)
                setTimeout(() => {
                    this.classList.remove('loading');
                }, 2000);
            });

            // Validación básica visual
            formControls.forEach(control => {
                control.addEventListener('blur', function () {
                    if (this.value.trim() === '') {
                        this.classList.add('error');
                        this.classList.remove('success');
                    } else {
                        this.classList.add('success');
                        this.classList.remove('error');
                    }
                });
            });
        });
    </script>
</asp:Content>
