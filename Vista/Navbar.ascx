<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Seguridad_JSC.Vista.Navbar" %>

<link rel="icon" type="image/x-icon" href="..\Vista\Recursos\Icon-1.jpg" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" />
<link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<link href="..\Vista\css\index.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />


<nav class="navbar navbar-expand-lg navbar-custom sticky-top">
    <div class="container">
        <a class="navbar-brand" href="#">
            <img src="..\Vista\Recursos\icon1.png" alt="Logo" width="60" height="60" class="me-2 animate-bounce" />
            <span class="animate-bounce">SEGURIDAD JSC S.A.S.</span>
        </a>



        <button id="btn-toggler" class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav ms-auto">
                <li class="nav-item"><a class="nav-link" href="/index.aspx">Inicio</a></li>
                <li class="nav-item"><a class="nav-link" href="/Vista/Productos.aspx">Nuestros Productos</a></li>
                <li class="nav-item"><a class="nav-link" href="/Vista/Cotizacion.aspx">Cotizacion</a></li>
                <li class="nav-item">
                    <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#loginModal">Iniciar Sesión</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h2 id="h2Login">LOGIN</h2>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="PanelLogin" runat="server">
                            <div class="mb-3">
                                <a href="/index.aspx">
                                    <img id="imgModal" src="..\Vista\Recursos\icon1.png" alt="Perfil" /><br />
                                </a>
                                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" Placeholder="Usuario(Email@)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Contraseña" TextMode="Password" />
                                    <button type="button" class="btn input-group-text" id="togglePassword">
                                        <i class="bi bi-eye-slash-fill"></i>
                                    </button>
                                </div>
                            </div>
                            <br />

                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" ClientIDMode="Static" />

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hfUserRole" runat="server" />

<style>
    #togglePassword {
        border: none;
    }

        #togglePassword i {
            color: white !important; /* Icono blanco */
            font-size: 1.2rem; /* Tamaño del icono */
        }


    /* 🔹 Modal ultra-transparente con borde animado */
    .modal-content {
        background: rgba(300, 300, 300, 0.1); /* Fondo más transparente */
        backdrop-filter: blur(10px); /* Efecto de desenfoque */
        color: #fff; /* Texto blanco */
        border-radius: 12px; /* Bordes redondeados */
        padding: 20px;
        position: relative;
        border: 3px solid transparent; /* Borde inicial */
        animation: borderGlow 1s infinite linear;
        filter: drop-shadow(0px 0px 12px grey)
    }

    .btn-close {
        background-color: white;
        color: white; /* Hace el icono blanco */
    }


    @keyframes borderGlow {
        0% {
            border-color: #ff5733;
        }
        /* Naranja */
        25% {
            border-color: #ff33a6;
        }
        /* Rosa */
        50% {
            border-color: #33ff57;
        }
        /* Verde */
        75% {
            border-color: #33a6ff;
        }
        /* Azul */
        100% {
            border-color: #ff5733;
        }
    }

    #h2Login {
        text-align: center;
        display: block;
        margin: 0 auto;
    }

    #btnLogin {
        background: rgba(255, 255, 255, 0.2); /* Fondo semi-transparente */
        display: block;
        margin: 0 auto;
        color: white; /* Texto blanco */
        padding: 10px 20px;
        border: 3px solid transparent; /* Borde inicial transparente */
        border-radius: 8px; /* Bordes redondeados */
        font-size: 16px;
        cursor: pointer;
        transition: background-color 0.3s ease-in-out, color 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
        animation: borderGlowBtn 2s infinite linear;
    }

        #btnLogin:hover {
            background: rgba(255, 255, 255, 0.9); /* Se vuelve blanco */
            color: black; /* Texto negro */
            box-shadow: 0 0 12px rgba(255, 255, 255, 0.8); /* Efecto de brillo */
        }

        #btnLogin:focus {
            outline: none;
            box-shadow: 0 0 15px rgba(0, 123, 255, 0.8); /* Efecto de brillo azul al hacer clic */
        }

    /* Animación del borde de colores */
    @keyframes borderGlowBtn {
        0% {
            border-color: transparent; /* Naranja */
        }

        25% {
            border-color: white; /* Azul */
        }

        50% {
            border-color: transparent; /* Verde */
        }

        75% {
            border-color: white; /* Rosa */
        }

        100% {
            border-color: transparent; /* Naranja */
        }
    }

    #txtUsuario {
        background-color: rgba(255, 255, 255, 0.2); /* Fondo semi-transparente */
        color: white; /* Texto blanco */
        border: 2px solid transparent; /* Borde inicial transparente */
        padding: 8px;
        border-radius: 12px;
        outline: none;
        transition: background-color 0.3s ease-in-out, border-color 0.3s ease-in-out, color 0.3s ease-in-out;
    }

    #txtPassword {
        background-color: rgba(255, 255, 255, 0.2); /* Fondo semi-transparente */
        color: white; /* Texto blanco */
        border: 2px solid transparent; /* Borde inicial transparente */
        padding: 8px;
        border-radius: 12px;
        outline: none;
        transition: background-color 0.3s ease-in-out, border-color 0.3s ease-in-out, color 0.3s ease-in-out;
    }

    #txtUsuario::placeholder {
        color: white; /* Placeholder blanco */
        opacity: 1; /* Asegura que se vea completamente */
    }

    #txtPassword::placeholder {
        color: white; /* Placeholder blanco */
        opacity: 1; /* Asegura que se vea completamente */
    }


    #txtUsuario:hover {
        color: black; /* Texto negro */
    }

    #txtPassword:hover {
        color: black; /* Texto negro */
    }

    #txtUsuario:focus {
        background-color: rgba(255, 255, 255, 0.9); /* Más blanco al escribir */
        color: black; /* Texto negro */
        border-color: #007bff; /* Azul llamativo */
        box-shadow: 0 0 8px rgba(0, 123, 255, 0.8); /* Brillo azul alrededor */
    }

    #txtPassword:focus {
        background-color: rgba(255, 255, 255, 0.9); /* Más blanco al escribir */
        color: black; /* Texto negro */
        border-color: #007bff; /* Azul llamativo */
        box-shadow: 0 0 8px rgba(0, 123, 255, 0.8); /* Brillo azul alrededor */
    }


    #imgModal {
        background-color: rgba(255, 255, 255, 0.6); /* Blanco semi-transparente */
        width: 120px;
        height: 120px;
        border-radius: 50%; /* Hace la imagen circular */
        object-fit: cover; /* Ajusta la imagen sin distorsión */
        border: 3px solid white; /* Borde blanco */
        display: block;
        margin: 0 auto;
        transition: background-color 0.3s ease-in-out; /* Efecto suave */
    }

        #imgModal:hover {
            background-color: rgba(255, 255, 255, 1); /* Se vuelve blanco al pasar el mouse */
        }

    .modal-header {
        height: 50px;
        text-align: center;
    }
</style>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("togglePassword").addEventListener("click", function () {
            var passwordInput = document.getElementById('<%= txtPassword.ClientID %>');
            var icon = this.querySelector("i");

            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                icon.classList.remove("bi-eye-slash-fill");
                icon.classList.add("bi-eye-fill");
            } else {
                passwordInput.type = "password";
                icon.classList.remove("bi-eye-fill");
                icon.classList.add("bi-eye-slash-fill");
            }
        });
    });
</script>
