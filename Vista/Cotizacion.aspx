<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizacion.aspx.cs" Inherits="Seguridad_JSC.Vista.Cotizacion" %>

<%@ Register Src="~/Vista/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="..\Vista\css\Cotizacion.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISdHljTUfRw5M5LMxOjFfjCiq3M9Ak53xv5JIz4eOook7moMJq8z2" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <uc:Navbar ID="NavbarControl" runat="server" />
            <h2 style="color: white; text-align: center">Solicitud de Cotización</h2>
            <br />
            <br />

            <div id="formContainer">
                <div class="form-group">
                    <label style="color: white" for="nombreCliente">Nombres del Cliente:</label>
                    <asp:TextBox ID="TxtNombreCliente" runat="server" placeholder="Escribe aquí..."></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="ApellidoCliente" style="color: white">Apellidos</label>
                    <asp:TextBox ID="TxtApellidoCliente" runat="server" placeholder="Escribe aquí..."></asp:TextBox>
                </div>
                <div class="form-group">
                    <label style="color: white" for="documentoCliente">Numero Documento:</label>
                    <asp:TextBox ID="txtDocumento" TextMode="Number"  runat="server" placeholder="Escribe aquí..."></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="telefonoCliente" style="color: white">Teléfono:</label>
                    <asp:TextBox ID="TxtTelefono" TextMode="Number" runat="server" placeholder="3¨¨¨¨"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="emailCliente" style="color: white">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="seguridad@gmail.com"></asp:TextBox>

                </div>
                <div class="form-group">
                    <label for="direccionInstalacion" style="color: white">Dirección de Instalación:</label>
                    <asp:TextBox ID="txtDireccion" runat="server" placeholder="Paipa-C#12-12"></asp:TextBox>

                </div>

                <div class="form-group">
                    <label for="tipoProducto" style="color: white">Tipo de Producto:</label>
                    <asp:DropDownList ID="ddlListaProductos" runat="server" CssClass="form-select" Style="text-align: center"></asp:DropDownList>

                </div>

                <div class="form-group">
                    <label for="cantidad" style="color: white">Cantidad:</label>
                    <asp:TextBox ID="TxtCantidad" runat="server" placeholder="1"></asp:TextBox>
                </div>

                <div id="containerObservaciones" class="form-group">
                    <label for="Detalles_Adicionales" style="color: white">Detalles Adicionales:</label>
                    <asp:TextBox ID="TxtObservaciones" runat="server" TextMode="MultiLine"
                        Style="width: 100%; height: 100px; padding: 10px; box-sizing: border-box;"
                        placeholder="Escribe aquí detalles adicionales de tu cotización o acláranos que estás interesado en más de nuestros productos">
                    </asp:TextBox>
                </div>

                <div id="DivRegistroCotizacion">
                    <asp:Button ID="BtnRegistrarCotizacion" runat="server" CssClass="registro-btn" Text="Realizar Cotizacion" OnClick="BtnRegistrarCotizacion_Click" />
                </div>
                <br />
                <br />
                <br />
                <br />

            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-Piv4xVNRyMGpqkFttMnVz0b6JkvoTRiZ4zAmwG7w9tBiw2fM9a6I5twW3q6odl6j" crossorigin="anonymous"></script>

</body>
</html>
