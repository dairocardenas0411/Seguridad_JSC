<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="Instalacion.aspx.cs" Inherits="Seguridad_JSC.Vista.Instalacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="text-center">
     <h1 class="title">Cotizaciones</h1>
 </div>

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
</asp:Content>
