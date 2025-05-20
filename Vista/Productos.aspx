<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Seguridad_JSC.Vista.Productos" %>

<%@ Register Src="~/Vista/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Productos</title>
    <link rel="icon" type="image/x-icon" href="..\Vista\Recursos\Icon-1.jpg" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="..\Vista\css\index.css" rel="stylesheet" />
    <link href="..\Vista\css\productos.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <uc:Navbar ID="NavbarControl" runat="server" />

            <div class="contenedorCarrusel">
                <div class="carrusel">
                    <div class="card">
                        <a href="index.aspx">
                            <img src="..\Vista\Recursos\carrusel-3.jpg" />
                        </a>
                    </div>

                    <div class="card">
                        <a href="index.aspx">
                            <img src="..\Vista\Recursos\carrusel-5.jpg" />
                        </a>
                    </div>

                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel.jpg" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel-2.jpg" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\icon1.png" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel-3.jpg" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel-5.jpg" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel.jpg" />
                    </div>
                    <div class="card">
                        <img src="..\Vista\Recursos\carrusel-2.jpg" />
                    </div>
                </div>

            </div>


            <br />
            <div id="socialButtons">
                <button id="btnWhatsApp" onclick="window.open('https://wa.me/3138797934', '_blank')"></button>
                <a id="btnFacebook" href="https://www.facebook.com/share/15n6DbrzgX/" target="_blank">
                    <i class="fa-brands fa-facebook-f"></i>
                </a>



            </div>
            <div id="headerContainer">
                <h2 id="tlCategorias">Nuestros Productos</h2>
                <div id="contensearch">
                    <input type="text" id="searchInput" placeholder="Buscar producto..." />
                    <button type="submit" id="searchBtn">Buscar</button>
                </div>
            </div>


            <br />
            <br />
            <h4 id="tlCamaras">Camaras</h4>
            <br />

            <div id="container">
                <div id="card">
                    <img src="..\Vista\Recursos\carrusel-5.jpg" alt="Imagen 1" />
                    <div id="card-content">
                        <h3>Cámara Cruiser SE+ DISPONIBLE FULL COLOR</h3>
                        <p>Descripción breve del producto</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto1.jpg" alt="Imagen 2" />
                    <div id="card-content">
                        <h3>Título 2</h3>
                        <p>Descripción breve de la tarjeta..</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\imgContainer.jpg" alt="Imagen 3" />
                    <div id="card-content">
                        <h3>Producto 3</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto2.jpg" alt="Imagen 4" />
                    <div id="card-content">
                        <h3>Título 4</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto3.jpg" alt="Imagen 5" />
                    <div id="card-content">
                        <h3>Título 5</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>
                <div id="card">
                    <img src="..\Vista\Recursos\carrusel-5.jpg" alt="Imagen 1" />
                    <div id="card-content">
                        <h3>Cámara Cruiser SE+ DISPONIBLE FULL COLOR</h3>
                        <p>Descripción breve del producto</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto1.jpg" alt="Imagen 2" />
                    <div id="card-content">
                        <h3>Título 2</h3>
                        <p>Descripción breve de la tarjeta..</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>
            </div>
            <br />
            <h4 id="tlCamaras">Alarmas</h4>
            <br />

            <div id="container">
                <div id="card">
                    <img src="..\Vista\Recursos\carrusel-5.jpg" alt="Imagen 1" />
                    <div id="card-content">
                        <h3>Cámara Cruiser SE+ DISPONIBLE FULL COLOR</h3>
                        <p>Descripción breve del producto</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto1.jpg" alt="Imagen 2" />
                    <div id="card-content">
                        <h3>Título 2</h3>
                        <p>Descripción breve de la tarjeta..</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\imgContainer.jpg" alt="Imagen 3" />
                    <div id="card-content">
                        <h3>Producto 3</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto2.jpg" alt="Imagen 4" />
                    <div id="card-content">
                        <h3>Título 4</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto3.jpg" alt="Imagen 5" />
                    <div id="card-content">
                        <h3>Título 5</h3>
                        <p>Descripción breve de la tarjeta.</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>
                <div id="card">
                    <img src="..\Vista\Recursos\carrusel-5.jpg" alt="Imagen 1" />
                    <div id="card-content">
                        <h3>Cámara Cruiser SE+ DISPONIBLE FULL COLOR</h3>
                        <p>Descripción breve del producto</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>

                <div id="card">
                    <img src="..\Vista\Recursos\producto1.jpg" alt="Imagen 2" />
                    <div id="card-content">
                        <h3>Título 2</h3>
                        <p>Descripción breve de la tarjeta..</p>
                        <a href="#" id="btn">Ver más</a>
                    </div>
                </div>
            </div>
        </div>
        <style>
            .card {
                flex: 0 0 250px;
                background-color: transparent;
                padding: 12px;
                text-align: center;
                border-radius: 15px;
                box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
                transition: transform 0.3s;
            }

                .card img {
                    padding: 12px;
                    width: 100%;
                    height: 100%;
                    object-fit: cover;
                    border-radius: 30px;
                    border: 2px solid #111111;
                    animation: borderAnimation 2s infinite linear;
                }

                .card:hover {
                    transform: scale(1.1);
                    background-color: transparent;
                    border-radius: 30px;
                    filter: drop-shadow(0px 0px 12px grey)
                }
        </style>


        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/js/all.min.js" crossorigin="anonymous"></script>
    </form>

</body>
</html>
