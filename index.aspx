<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Seguridad_JSC.index" %>
<%@ Register Src="~/Vista/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Seguridad JSC</title>
    <link rel="icon" type="image/x-icon" href="..\Vista\Recursos\Icon-1.jpg" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="..\Vista\css\index.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />


</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <uc:Navbar ID="NavbarControl" runat="server" />

            <style>
                * {
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                }

                body {
                    font-family: Arial, sans-serif;
                    overflow-x: hidden;
                }

                .container {
                    position: relative;
                    height: 100vh;
                    overflow: hidden;
                }

                .image-section {
                    position: fixed;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100vh;
                    background: url('../Vista/Recursos/Index.jpg') no-repeat center center;
                    background-size: cover; /* Ajusta la imagen para cubrir toda la pantalla */
                    transition: transform 0.9s ease, opacity 0.9s ease;
                    z-index: -1;
                }

                * {
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                }


                .containerInfo {
                    width: 100%;
                    padding: 50px 10%;
                    background-color: transparent;
                    color: white;
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    text-align: center;
                }

                .row-info {
                    display: flex;
                    flex-wrap: wrap;
                    justify-content: space-between; /* Distribuye el contenido equitativamente */
                    align-items: center; /* Alinea verticalmente */
                    width: 100%;
                }

                .column-info {
                    flex: 1; /* Hace que ambas columnas ocupen el mismo espacio */
                    max-width: 48%; /* Controla el tamaño de cada columna */
                    padding: 20px;
                    box-sizing: border-box;
                    text-align: center;
                }

                    .image-gallery img,
                    .column-info img {
                        width: 100%;
                        max-height: 300px; /* Ajusta la altura máxima para mantener consistencia */
                        object-fit: cover;
                        border-radius: 20px;
                        display: block;
                        margin: 10px auto; /* Mantiene las imágenes centradas */
                    }

                @media (max-width: 768px) {
                    .row-info {
                        flex-direction: column;
                    }

                    .column-info {
                        max-width: 100%;
                    }
                }

                
            </style>

            <div class="container" id="fadeContainer">
                <div class="image-section" id="image"></div>
            </div>
            <div class="containerInfo">
                <div class="row-info">
                    <div class="column-info col-info">
                        <h2>¿Quiénes Somos?</h2>
                        <br />
                        <p>
                            Seguridad JSC SAS es una empresa especializada en la instalación y venta de cámaras de seguridad,
                    sistemas de vigilancia y equipos de monitoreo. Nuestro objetivo es brindar tranquilidad y
                    protección a hogares y empresas con tecnología avanzada.
                        </p>
                        <br />
                        <br />
                        <br />
                        <img src="../Vista/Recursos/instalacion2.jpg" class="fade-effect" /><br />
                        <br />

                    </div>

                    <!-- Columna 2: Imágenes llamativas con efecto de desvanecimiento -->
                    <div class="column-info image-gallery">
                        <img src="../Vista/Recursos/nosotros.jpg" class="fade-effect" /><br />
                        <br />
                        <h2>Instalación Profesional de Cámaras de Seguridad</h2>
                        <p>
                            En Seguridad JSC SAS, nos especializamos en la instalación y configuración de sistemas de videovigilancia para hogares,
                            empresas y establecimientos comerciales. Nuestro equipo de técnicos altamente capacitados garantiza un servicio eficiente y seguro,
                            adaptado a las necesidades de cada cliente.

                        </p>
                    </div>
                </div>
            </div>




        </div>
        <asp:HiddenField ID="hfUserRole" runat="server" />
        <div id="socialButtons">
            <button id="btnWhatsApp" onclick="window.open('https://wa.me/3138797934', '_blank')"></button>
            <a id="btnFacebook" href="https://www.facebook.com/share/15n6DbrzgX/" target="_blank">
                <i class="fa-brands fa-facebook-f"></i>
            </a>
        </div>
        <script>
            window.history.forward();
            function preventBack() {
                window.history.forward();
            }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
        </script>
        <script>

            window.addEventListener('scroll', function () {
                let scrollPosition = window.scrollY;
                let image = document.getElementById('image');
                let scaleFactor = 1 + scrollPosition / 500;
                let fadePoint = 200;

                image.style.transform = `scale(${scaleFactor})`;
                image.style.opacity = Math.max(1 - scrollPosition / fadePoint, 0);
            }); </script>
      

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/js/all.min.js" crossorigin="anonymous"></script>
    </form>
</body>
</html>
