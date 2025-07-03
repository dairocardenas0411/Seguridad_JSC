<%@ Page Title="Productos del Proveedor" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="ListaProductosProveedor.aspx.cs" Inherits="Seguridad_JSC.Vista.ListaProductosProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <link href="../Vista/css/ProductosProveedor.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="searchBox">
        <div class="filas-wrapper">
            <label id="lblFilas">Número de filas por página</label>
            <asp:TextBox ID="TxtNumeroFilas" runat="server" TextMode="Number" placeholder="100" CssClass="form-control" ClientIDMode="Static" />
            <asp:Button ID="btnAplicarNumeroFilas" runat="server" Text="Aplicar" CssClass="btn" OnClick="btnAplicarNumero_Click" ClientIDMode="Static" />

            <div class="search-input-container">
                <input type="text" id="searchInput" onkeyup="filtrarTabla()" placeholder="Buscar por nombre, categoría o estado..." />
                <i id="searchIcon" class="fas fa-search"></i>
            </div>
        </div>
    </div>

    <div class="table-container mt-4">
        <asp:Label ID="lblSinProductos" runat="server" CssClass="alert alert-warning"
            Text="No se encontraron productos registrados para este proveedor." Visible="false" />

        <asp:Repeater ID="rptProductos" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover" id="tablaProducto">
                    <thead>
                        <tr>
                            <th>Imagen</th>
                            <th>Producto</th>
                            <th>Descripción</th>
                            <th>Valor Unitario</th>
                            <th>Stock</th>
                            <th>Estado</th>
                            <th>Categoría</th>
                            <th>Accion</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%# ResolveUrl("~/Vista/Recursos/" + Eval("imagen")) %>' alt="Imagen Producto" width="70" height="70" style="object-fit: cover; border-radius: 8px;" />
                    </td>
                    <td><%# Eval("nombreProducto") %></td>
                    <td><%# Eval("descripcion") %></td>
                    <td>$<%# Eval("precioUnitario", "{0:N0}") %></td>
                    <td><%# Eval("cantidadStock") %></td>
                    <td><%# Eval("estado") %></td>
                    <td><%# Eval("nombreCategoria") %></td>
                    <td>
                        <asp:Button ID="btnProductos" runat="server" CssClass="btn-admin btn-Perfil" Text="Ver Producto"
                            CommandArgument='<%# Eval("idProducto") %>'  /></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="pagination-container">
            <asp:Button ID="btnPrev" runat="server" Text="« Anterior" OnClick="btnPrev_Click" CssClass="btn btn-outline-secondary btn-sm" />
            <asp:Label ID="lblPageInfo" runat="server" Text="" Style="margin: 0 15px;"></asp:Label>
            <asp:Button ID="btnNext" runat="server" Text="Siguiente »" OnClick="btnNext_Click" CssClass="btn btn-outline-secondary btn-sm" />
        </div>
    </div>

    <script src="..\Vista\js\ListaProductosProveedor.js"></script>
</asp:Content>
