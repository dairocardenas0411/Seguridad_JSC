<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="ListaProveedores.aspx.cs" Inherits="Seguridad_JSC.Vista.ListaProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <link href="../Vista/css/ListaProveedores.css" rel="stylesheet" />

    <div id="searchBox">
        <div class="filas-wrapper">
            <label id="lblFilas">Número de filas por página</label>
            <asp:TextBox ID="TxtNumeroColm" runat="server" TextMode="Number" placeholder="100" CssClass="form-control" ClientIDMode="Static" />
            <asp:Button ID="btnAplicarNumero" runat="server" Text="Aplicar" CssClass="btn" OnClick="btnAplicarNumero_Click" ClientIDMode="Static" />

            <div class="search-input-container">
                <input type="text" id="searchInput" onkeyup="filtrarTabla()" placeholder="Buscar por nombre, documento o empresa..." />
                <i id="searchIcon" class="fas fa-search"></i>
            </div>
        </div>
    </div>

    <div class="table-container mt-4">
        <asp:Repeater ID="rptProveedores" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover" id="tablaProveedores">
                    <thead>
                        <tr>
                            <th>Foto</th>
                            <th>Nombre</th>
                            <th>Documento</th>
                            <th>Empresa</th>
                            <th>Email</th>
                            <th>Celular</th>
                            <th>Productos</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HiddenField ID="hfImagenActual" runat="server" Value='<%# Eval("imagen") %>' />
                <tr>
                    <td>
                        <img src='<%# ResolveUrl("~/Vista/Recursos/" + Eval("imagen")) %>' alt="Foto Proveedor"
                            width="60" height="60" style="object-fit: cover; border-radius: 50%; display: block; margin-bottom: 5px;" />
                        <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control-file form-control-img" />
                    </td>

                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"
                            Text='<%# Eval("nombre") %>' />
                    </td>

                    <td>
                        <asp:TextBox ID="txtDocumento" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"
                            Text='<%# Eval("documento") %>' />
                    </td>

                    <td>
                        <asp:TextBox ID="txtEmpresa" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"
                            Text='<%# Eval("empresa") %>' />
                    </td>

                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"
                            Text='<%# Eval("email") %>' />
                    </td>

                    <td>
                        <asp:TextBox ID="txtCelular" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"
                            Text='<%# Eval("celular") %>' />
                    </td>

                    <td><%# Eval("totalProductos") %></td>

                    <td>
                        <asp:Button ID="btnGuardarProveedor" runat="server" CssClass="btn-admin btn-Perfil" Text="Guardar"
                            CommandArgument='<%# Eval("idProveedor") %>' OnCommand="btnGuardarProveedor_Command" />
                        <asp:Button ID="btnProductos" runat="server" CssClass="btn-admin btn-Ver" Text="Productos"
                            CommandArgument='<%# Eval("idProveedor") %>' OnCommand="btnProductos_Command" />
                    </td>
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

    <!-- Script para filtrar tabla -->
    <script src="..\Vista\js\ListaProveedores.js"></script>
</asp:Content>
