<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="ListaCotizaciones.aspx.cs" Inherits="Seguridad_JSC.Vista.ListaCotizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="..\Vista\css\UserCotizacion.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <asp:Label ID="LblidUsuario" runat="server" Text="Label" Visible="false"></asp:Label>

    <div id="searchBox">
        <div class="filas-wrapper">
            <label id="lblFilas">Número de filas por página</label>
            <asp:TextBox ID="TxtNumeroColm" runat="server" TextMode="Number" placeholder="100" CssClass="form-control" ClientIDMode="Static" />
            <asp:Button ID="btnAplicarNumero" runat="server" Text="Aplicar" CssClass="btn" OnClick="btnAplicarNumero_Click" ClientIDMode="Static" />

            <div class="search-input-container">
                <input type="text" id="searchInput" onkeyup="filtrarTabla()" placeholder="Buscar por nombre, correo o producto..." />
                <i id="searchIcon" class="fas fa-search"></i>
            </div>
        </div>


    </div>

    <div class="table-container mt-4">
        <asp:Repeater ID="rptCotizaciones" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover" id="tablaCotizaciones">
                    <thead>
                        <tr>
                            <th>Nombres</th>
                            <th>Correo</th>
                            <th>Dirección</th>
                            <th>Observaciones</th>
                            <th>Fecha</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("NombreCliente") %></td>
                    <td><%# Eval("email") %></td>
                    <td ><%# Eval("DireccionInstalacion") %></td>
                    <td ><%# Eval("Observaciones") %></td>
                    <td><%# Eval("fechaCotizacion") %></td>
                    <td>
                        <div class="btn-container">
                                <asp:Button ID="btnVerCotizacion" runat="server" CssClass="btn-admin btn-ver" Text="📜 Ver Detalles"
                                    CommandArgument='<%# Eval("idCotizacion") %>' OnCommand="btnVerCotizacion_Command" />

                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandArgument='<%# Eval("idCotizacion") %>'
                                    CommandName="Eliminar"
                                    OnCommand="btnEliminar_Command"
                                    OnClientClick='<%# "return confirmarEliminacion(" + Eval("idCotizacion") + ");" %>'
                                    CssClass="btn-admin btn-eliminar">🗑 Eliminar</asp:LinkButton>
                        </div>
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



    <!-- js -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script src="..\Vista\js\JavaScript.js"></script>



</asp:Content>
