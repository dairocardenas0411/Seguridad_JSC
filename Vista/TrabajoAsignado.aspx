<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="TrabajoAsignado.aspx.cs" Inherits="Seguridad_JSC.Vista.TrabajoAsignado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="..\Vista\css\Cotizacionprint.css" rel="stylesheet" />
    <link href="..\Vista\css\UserCotizacion.css" rel="stylesheet" />


    <asp:Label ID="LblidUsuario" runat="server" Text="Label" Visible="false"></asp:Label>

    <div id="searchBox">
        <label id="lblFilas">Número de filas por página</label>
        <asp:TextBox ID="TxtNumeroColm" runat="server" TextMode="Number" placeholder="100" CssClass="form-control" ClientIDMode="Static" />
        <asp:Button ID="btnAplicarNumero" runat="server" Text="Aplicar" CssClass="btn" OnClick="btnAplicarNumero_Click" ClientIDMode="Static" />

        <div class="search-input-container">
            <input type="text" id="searchInput" onkeyup="filtrarTabla()" placeholder="Buscar por nombre, correo o producto..." />
            <i id="searchIcon" class="fas fa-search"></i>
        </div>
    </div>



    <div class="table-container mt-4">
        <asp:Repeater ID="rptCotizaciones" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover" id="tablaCotizaciones">
                    <thead>
                        <tr>
                            <th>Nombre Cliente</th>
                            <th>Dirección</th>
                            <th>Telefono</th>
                            <th>Tipo Trabajo</th>
                            <th>Observaciones</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("NombreCliente") %> <%# Eval("apellidoCliente") %></td>
                    <td><%# Eval("DireccionInstalacion") %></td>
                    <td><%# Eval("telefono") %></td>
                    <td><%# Eval("TipoTrabajo") %></td>

                    <td style="width: 400px;">
                        <asp:TextBox ID="txtObservacionTrabajo" runat="server"
                            Text='<%# Eval("observacionesTrabajo") %>'
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="5"
                            Style="resize: horizontal; height: 100px; width: 100%;"></asp:TextBox>
                    </td>


                    <td>

                        <asp:Button ID="btnVerCotizacion" runat="server" CssClass="btn-admin btn-ver" Text="Informacion"
                            CommandArgument='<%# Eval("idCotizacion") %>' OnCommand="btnVerCotizacion_Command" />

                        <asp:Button ID="bntFinalizar" runat="server" Text="✅ Finalizar"
                            CssClass="btn-admin btn-Finalizar" CommandName="Finalizar"
                            CommandArgument='<%# Eval("idCotizacion") %>' OnClick="bntFinalizar_Click" />

                        <span class="tooltip-text">Ver Detalles</span>
                        <asp:Button ID="btnObservacionesT" runat="server" Text="Guardar Observación"
                            CssClass="btn-admin btn-ver" CommandName="GuardarObservacion"
                            CommandArgument='<%# Eval("idCotizacion") %>' OnClick="btnObservacionesT_Click" />
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
