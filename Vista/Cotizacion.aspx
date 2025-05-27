<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizacion.aspx.cs" Inherits="Seguridad_JSC.Vista.Cotizacion" %>
<%@ Register Src="~/Vista/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Solicitud de Cotización - Seguridad JSC</title>
    
    <!-- CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISdHljTUfRw5M5LMxOjFfjCiq3M9Ak53xv5JIz4eOook7moMJq8z2" crossorigin="anonymous" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Vista/css/Cotizacion.css" rel="stylesheet" />
    
    <!-- JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <uc:Navbar ID="NavbarControl" runat="server" />
        
        <h1 class="page-title">Solicitud de Cotización</h1>

        <div id="formContainer" class="container">
            <!-- Sección: Datos del Cliente -->
            <div class="card mb-4">
                <div class="card-header">
                    <h4><i class="fas fa-user"></i> Información del Cliente</h4>
                </div>
                <div class="card-body">
                    <div class="row g-4">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="TxtNombreCliente">Nombres del Cliente</label>
                                <asp:TextBox ID="TxtNombreCliente" runat="server" CssClass="form-control" 
                                    placeholder="Ingresa tu nombre completo..." />
                            </div>
                            
                            <div class="form-group">
                                <label for="txtDocumento">Número de Documento</label>
                                <asp:TextBox ID="txtDocumento" TextMode="Number" runat="server" CssClass="form-control" 
                                    placeholder="Número de identificación..." />
                            </div>
                            
                            <div class="form-group">
                                <label for="TxtTelefono">Teléfono de Contacto</label>
                                <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control" 
                                    placeholder="300 123 4567..." />
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="TxtApellidoCliente">Apellidos</label>
                                <asp:TextBox ID="TxtApellidoCliente" runat="server" CssClass="form-control" 
                                    placeholder="Ingresa tus apellidos..." />
                            </div>
                            
                            <div class="form-group">
                                <label for="txtEmail">Correo Electrónico</label>
                                <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" 
                                    placeholder="tu.email@ejemplo.com" />
                            </div>
                            
                            <div class="form-group">
                                <label for="txtDireccion">Dirección de Instalación</label>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" 
                                    placeholder="Calle 123 #45-67, Ciudad..." />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sección: Productos -->
            <div class="card mb-4">
                <div class="card-header">
                    <h4><i class="fas fa-shopping-cart"></i> Productos a Cotizar</h4>
                </div>
                <div class="card-body">
                    <div class="row g-3 align-items-end">
                        <div class="col-md-5">
                            <label for="ddlListaProductos">Tipo de Producto</label>
                            <asp:DropDownList ID="ddlListaProductos" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Seleccione un producto --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="TxtCantidad">Cantidad</label>
                            <asp:TextBox ID="TxtCantidad" TextMode="Number" runat="server" CssClass="form-control" 
                                placeholder="1" min="1" value="1" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="BtnAgregarProducto" runat="server" CssClass="btn btn-primary w-100" 
                                Text="➕ Agregar Producto" OnClick="BtnAgregarProducto_Click" />
                        </div>
                    </div>

                    <!-- Lista de productos agregados -->
                    <div class="mt-4">
                        <h5>Productos Agregados:</h5>
                        <div id="productosContainer" class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanelProductos" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridViewProductos" runat="server" CssClass="table table-striped table-hover" 
                                        AutoGenerateColumns="false" OnRowCommand="GridViewProductos_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnEliminar" runat="server" 
                                                        CommandName="Eliminar" 
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CssClass="btn btn-danger btn-sm" 
                                                        Text="🗑️ Eliminar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div class="text-center text-muted py-3">
                                                <i class="fas fa-box-open fa-2x"></i>
                                                <p class="mt-2">No hay productos agregados</p>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnAgregarProducto" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sección: Información Adicional -->
            <div class="card mb-4">
                <div class="card-header">
                    <h4><i class="fas fa-info-circle"></i> Información Adicional</h4>
                </div>
                <div class="card-body">
                    <div class="form-group">
                       
                        <asp:TextBox ID="TxtObservaciones" runat="server" TextMode="MultiLine"
                            CssClass="form-control" Rows="4"
                            placeholder="Describe cualquier requerimiento específico, horarios preferidos para instalación, o información adicional que nos ayude a brindarte una mejor atención..." />
                    </div>
                </div>
            </div>

            <!-- Botón de envío -->
            <div class="text-center">
                <asp:Button ID="BtnRegistrarCotizacion" runat="server" CssClass="btn btn-success btn-lg px-5 registro-btn" 
                    Text="✨ Solicitar Cotización" OnClick="BtnRegistrarCotizacion_Click" />
            </div>
        </div>
    </form>

    <!-- JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-Piv4xVNRyMGpqkFttMnVz0b6JkvoTRiZ4zAmwG7w9tBiw2fM9a6I5twW3q6odl6j" 
            crossorigin="anonymous"></script>
    <script src="../Vista/js/jsCotizacion.js"></script>
</body>
</html>