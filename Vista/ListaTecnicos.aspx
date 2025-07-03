<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="ListaTecnicos.aspx.cs" Inherits="Seguridad_JSC.Vista.ListaTecnicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <link href="../Vista/css/ListaUsuarios.css" rel="stylesheet" />
    
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
        <asp:Repeater ID="rptTecnicos" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover" id="tablaTecnicos">
                    <thead>
                        <tr>
                            <th>Foto</th>
                            <th>Nombre</th>
                            <th>Documento</th>
                            <th>Telefono</th>
                            <th>Correo</th>
                            <th>Estado</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%# ResolveUrl("~/Vista/Recursos/" + Eval("foto")) %>' alt="Foto Técnico" width="80" height="80" style="object-fit: cover; border-radius: 50%;" />
                    </td>

                    <td><%# Eval("nombreUsuario") %></td>
                    <td><%# Eval("documento") %></td>
                    <td><%# Eval("celular") %></td>
                    <td><%# Eval("email") %></td>
                    <td><%# Eval("estado") %></td>
                    <td>
                        <div class="btn-container">
                            <div class="tooltip-container" style="display: flex; gap: 10px;">
                                <asp:Button ID="btnEstado" runat="server" CssClass="btn-admin btn-ver" Text="Cambiar Estado"
                                    CommandName="CambiarEstado" CommandArgument='<%# Eval("idUsuario") %>' OnClick="btnEstado_Click" />

                                <asp:Button ID="btnPerfil" runat="server" CssClass="btn-admin btn-Perfil" Text="Editar Perfil"
                                    CommandArgument='<%# Eval("idUsuario") %>' OnCommand="btnPerfil_Command" />
                            </div>
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
     <script src="..\Vista\js\ListaUsuarioJS.js"></script>
    
</asp:Content>
