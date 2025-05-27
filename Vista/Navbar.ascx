<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Seguridad_JSC.Vista.Navbar" %>

<!-- ESTILOS Y LIBRERÍAS -->
<link rel="icon" type="image/x-icon" href="../Vista/Recursos/Icon-1.jpg" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
<link href="../Vista/css/index.css" rel="stylesheet" />

<!-- SCRIPTS -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<!-- NAVBAR -->
<nav class="navbar navbar-expand-lg navbar-custom sticky-top">
    <div class="container">
        <a class="navbar-brand d-flex align-items-center" href="#">
            <img src="../Vista/Recursos/icon1.png" alt="Logo" width="60" height="60" class="me-2 animate-bounce" />
            <span class="fs-4 animate-bounce">SEGURIDAD JSC S.A.S.</span>
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
            aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav ms-auto text-end">
                <li class="nav-item"><a class="nav-link" href="/index.aspx">Inicio</a></li>
                <li class="nav-item"><a class="nav-link" href="/Vista/Productos.aspx">Nuestros Productos</a></li>
                <li class="nav-item"><a class="nav-link" href="/Vista/Cotizacion.aspx">Cotización</a></li>
                <li class="nav-item">
                    <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#loginModal">Iniciar Sesión</a>
                </li>
            </ul>
        </div>
    </div>
</nav>

<!-- Modal Login -->
<div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
    <div class="modal-dialog d-flex align-items-center min-vh-100">
        <div class="modal-content glass-modal">
            <div class="modal-header border-0">
                <button type="button" class="btn-close bg-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body">
                <!-- UpdatePanel para el contenido del formulario -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PanelLogin" runat="server">
                            <div class="text-center mb-3">
                                <a href="/index.aspx">
                                    <img src="../Vista/Recursos/icon1.png" alt="Perfil" class="img-thumbnail rounded-circle" style="width: 100px; height: 100px;" />
                                </a>
                            </div>
                            <div class="mb-3">
                                <asp:TextBox ID="txtUsuario" CssClass="form-control glass-input" runat="server" 
                                    Placeholder="Usuario (Email@)" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="mb-3 input-group">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control glass-input" 
                                    placeholder="Contraseña" TextMode="Password" ClientIDMode="Static" />
                                <button type="button" class="btn btn-outline-light" id="togglePassword">
                                    <i class="bi bi-eye-slash-fill"></i>
                                </button>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="Button1" runat="server" 
                                    CssClass="btn glass-button btn-sm px-4 py-2" 
                                    Text="Iniciar Sesión" 
                                    OnClick="btnLogin_Click" 
                                    ClientIDMode="Static"
                                    CausesValidation="false" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>

<asp:HiddenField ID="hfUserRole" runat="server" />
<style>
    body {
        overflow-x: hidden;
    }

    .glass-modal {
        background: rgba(255, 255, 255, 0.1);
        backdrop-filter: blur(12px);
        color: #fff;
        border-radius: 12px;
        padding: 20px;
        animation: borderGlow 3s infinite linear;
        border: 3px solid transparent;
        box-shadow: 0 0 12px rgba(255, 255, 255, 0.2);
    }

    .glass-input {
        background: rgba(255, 255, 255, 0.2);
        color: white;
        border: 1px solid transparent;
        border-radius: 10px;
        transition: 0.3s ease;
    }

        .glass-input:focus {
            background: rgba(255, 255, 255, 0.95);
            color: black;
            border-color: #007bff;
            box-shadow: 0 0 10px rgba(0, 123, 255, 0.5);
        }

        .glass-input::placeholder {
            color: white;
            opacity: 0.8;
        }

    .glass-button {
        background: rgba(255, 255, 255, 0.2);
        color: white;
        border: 2px solid transparent;
        border-radius: 10px;
        transition: 0.3s ease;
        animation: borderGlowBtn 2s infinite linear;
    }

        .glass-button:hover {
            background: rgba(255, 255, 255, 0.9);
            color: black;
            box-shadow: 0 0 12px rgba(255, 255, 255, 0.8);
        }

        .glass-button.btn-sm {
            font-size: 0.9rem;
            padding: 0.4rem 1.2rem;
            border-radius: 8px;
        }

    @keyframes borderGlow {
        0% {
            border-color: #ff5733;
        }

        25% {
            border-color: #ff33a6;
        }

        50% {
            border-color: #33ff57;
        }

        75% {
            border-color: #33a6ff;
        }

        100% {
            border-color: #ff5733;
        }
    }

    @keyframes borderGlowBtn {
        0%, 100% {
            border-color: transparent;
        }

        25%, 75% {
            border-color: white;
        }

        50% {
            border-color: transparent;
        }
    }

    .btn-close {
        background-color: white !important;
    }

    .modal-dialog {
        display: flex;
        align-items: center;
        min-height: 100vh;
    }

    /* ------- NAVBAR TOGGLE FIJO SUPERIOR IZQUIERDO Y COLOR LLAMATIVO ------- */
    .navbar-toggler {
        position: fixed;
        top: 10px;
        left: 10px;
        z-index: 1051;
        background-color: #ffffffdd;
        border: none;
        width: 45px;
        height: 45px;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(255, 255, 255, 0.9);
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        transition: background-color 0.3s ease;
    }

        .navbar-toggler:hover {
            background-color: #ffffffee;
        }

        .navbar-toggler:focus,
        .navbar-toggler:focus-visible,
        .navbar-toggler:active {
            outline: none !important;
            box-shadow: 0 0 12px 3px rgba(255, 255, 255, 0.8);
        }

    /* Cambia el icono de hamburguesa para que tenga color blanco */
    .navbar-toggler-icon {
        filter: invert(1) brightness(1.2);
    }

    /* Quitar línea azul en los links al hacer foco */
    .nav-link:focus,
    .nav-link:focus-visible,
    .nav-link:active {
        outline: none !important;
        box-shadow: none !important;
        border: none !important;
    }

    @media (max-width: 992px) {
        #navbarNav {
            position: fixed;
            top: 0;
            left: 60px;
            height: 100vh;
            width: 250px;
            background-color: rgba(0, 0, 0, 0.85);
            backdrop-filter: blur(8px);
            padding-top: 60px;
            overflow-y: auto;
            transition: transform 0.3s ease-in-out;
            transform: translateX(-100%);
            z-index: 1050;
        }

            #navbarNav.show {
                transform: translateX(0);
            }

            #navbarNav ul.navbar-nav {
                flex-direction: column;
                width: 100%;
                padding-left: 0;
                margin: 0;
            }

                #navbarNav ul.navbar-nav li.nav-item {
                    margin: 0;
                }

                    #navbarNav ul.navbar-nav li.nav-item a.nav-link {
                        color: white;
                        padding: 15px 20px;
                        border-bottom: 1px solid rgba(255, 255, 255, 0.1);
                    }

                        #navbarNav ul.navbar-nav li.nav-item a.nav-link:hover,
                        #navbarNav ul.navbar-nav li.nav-item a.nav-link:focus {
                            background-color: rgba(255, 255, 255, 0.15);
                            color: #fff;
                            outline: none;
                            box-shadow: none;
                        }
    }
</style>
<!-- SCRIPTS -->
<script>
    // Función para mostrar alertas (mantener por compatibilidad)
    function mostrarAlerta(titulo, mensaje, tipo) {
        Swal.fire({
            title: titulo,
            text: mensaje,
            icon: tipo,
            confirmButtonText: 'Aceptar'
        });
    }

    document.addEventListener("DOMContentLoaded", function () {
        initializeNavbarEvents();
    });

    // Función para inicializar eventos (se ejecuta también después de UpdatePanel)
    function initializeNavbarEvents() {
        // Toggle menú lateral con el botón toggle fijo
        const navbarToggler = document.querySelector(".navbar-toggler");
        const navbarNav = document.getElementById("navbarNav");

        if (navbarToggler && navbarNav) {
            navbarToggler.addEventListener("click", () => {
                navbarNav.classList.toggle("show");
            });
        }

        // Toggle mostrar/ocultar contraseña
        const togglePassword = document.getElementById("togglePassword");
        if (togglePassword) {
            togglePassword.addEventListener("click", function () {
                var passwordInput = document.getElementById('txtPassword');
                var icon = this.querySelector("i");

                if (passwordInput) {
                    if (passwordInput.type === "password") {
                        passwordInput.type = "text";
                        icon.classList.remove("bi-eye-slash-fill");
                        icon.classList.add("bi-eye-fill");
                    } else {
                        passwordInput.type = "password";
                        icon.classList.remove("bi-eye-fill");
                        icon.classList.add("bi-eye-slash-fill");
                    }
                }
            });
        }
    }

    // Re-inicializar eventos después de cada postback del UpdatePanel
    if (typeof Sys !== 'undefined') {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, args) {
            initializeNavbarEvents();
        });
    }

    // Prevenir que el modal se cierre al hacer clic fuera cuando hay una alerta activa
    $(document).ready(function () {
        $('#loginModal').on('show.bs.modal', function (e) {
            // Limpiar campos cuando se abre el modal
            $('#txtUsuario').val('');
            $('#txtPassword').val('');
        });
    });
</script>