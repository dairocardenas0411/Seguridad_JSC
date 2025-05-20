<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Seguridad_JSC.Vista.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <script>
      window.history.forward();
      function preventBack() {
          window.history.forward();
      }
      setTimeout("preventBack()", 0);
      window.onunload = function () { null };
      </script>

</asp:Content>
