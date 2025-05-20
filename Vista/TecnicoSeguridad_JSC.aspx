<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="TecnicoSeguridad_JSC.aspx.cs" Inherits="Seguridad_JSC.Vista.TecnicoSeguridad_JSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <nav class="navbar navbar-dark bg-dark p-3">
            <div class="container-fluid d-flex justify-content-end">
                <button class="btn btn-outline-light me-2"><i class="bi bi-person-circle"></i>Perfil</button>
                <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" Text="Cerrar Sesión" OnClick="btnLogout_Click" />
            </div>
        </nav>

        <div class="container mt-4">
            <h3 class="text-center">Instalaciones</h3>
            <table class="table table-striped table-bordered text-center">
                <thead class="table-dark">
                    <tr>
                        <th>Nombre</th>
                        <th>Instalación</th>
                        <th>Ubicación</th>
                        <th>Técnico</th>
                        <th>Estado</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Cliente 1</td>
                        <td>Cámaras</td>
                        <td>Bogotá</td>
                        <td>Juan Pérez</td>
                        <td><span class="badge bg-success">Completado</span></td>
                        <td>
                            <button class="btn btn-warning btn-sm"><i class="bi bi-pencil"></i>Registrar Gastos</button>
                            <button class="btn btn-info btn-sm"><i class="bi bi-file-earmark-text"></i>Generar Reporte</button>
                        </td>
                    </tr>
                    <tr>
                        <td>Cliente 2</td>
                        <td>Alarmas</td>
                        <td>Medellín</td>
                        <td>Juan Perez</td>
                        <td><span class="badge bg-primary">En Proceso</span></td>
                        <td>
                            <button class="btn btn-warning btn-sm"><i class="bi bi-pencil"></i>Registrar Gastos</button>
                            <button class="btn btn-info btn-sm"><i class="bi bi-file-earmark-text"></i>Generar Reporte</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <script>
            window.history.forward();
            function preventBack() {
                window.history.forward();
            }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
        </script>
        <script>
            window.history.forward();
            function preventBack() {
                window.history.forward();
            }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
        </script>

        <!-- Bootstrap Icons -->
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
    </div>
</asp:Content>
