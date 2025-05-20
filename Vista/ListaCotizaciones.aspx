<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="ListaCotizaciones.aspx.cs" Inherits="Seguridad_JSC.Vista.ListaCotizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .title {
            font-size: 2.2rem;
            font-weight: 700;
            color: #0a2f35;
            border-bottom: 4px solid #138d94;
            display: inline-block;
            padding-bottom: 6px;
            margin-bottom: 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .table-container {
            background-color: #fdfefe;
            padding: 25px;
            border-radius: 16px;
            box-shadow: 0 6px 18px rgba(0, 0, 0, 0.08);
            overflow-x: auto;
        }

        /* Estilo tabla general */
        .table {
            width: 100%;
            border-collapse: collapse;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 0.95rem;
            color: #2c3e50;
        }

        /* Cabecera */
        .table thead th {
            background-color: #0f4e54;
            color: #ffffff;
            padding: 14px 10px;
            text-align: center;
            font-weight: 600;
            border-bottom: 3px solid #138d94;
        }

        /* Cuerpo */
        .table tbody td {
            padding: 12px 10px;
            text-align: center;
            border-bottom: 1px solid #e0e0e0;
            vertical-align: middle;
        }

        /* Efecto hover */
        .table tbody tr:hover {
            background-color: #f0f8f8;
            transition: background-color 0.3s ease;
        }

        /* Botones */
        .btn {
            margin: 3px;
            border-radius: 6px;
            font-size: 0.85rem;
            padding: 6px 10px;
        }

        /* Badge de estado */
        .badge {
            font-size: 0.85rem;
            padding: 6px 10px;
            border-radius: 20px;
            font-weight: 500;
        }

        /* Responsive */
        @media (max-width: 768px) {
            .table-container {
                padding: 15px;
                border-radius: 12px;
            }

            .table {
                display: block;
                width: 100%;
                overflow-x: auto;
            }

            .table thead {
                display: none;
            }

            .table tbody td {
                display: block;
                width: 100%;
                text-align: right;
                padding: 10px 8px;
                position: relative;
                border-bottom: 1px solid #ddd;
            }

            .table tbody td::before {
                content: attr(data-label);
                position: absolute;
                left: 10px;
                width: 50%;
                text-align: left;
                font-weight: bold;
                color: #0a2f35;
            }

            .table tbody tr {
                margin-bottom: 15px;
                display: block;
                border-radius: 12px;
                background-color: #ffffff;
                box-shadow: 0 2px 8px rgba(0, 0, 0, 0.06);
            }

            /* Estilos para botones generales */
.btn {
    padding: 8px 14px;
    border-radius: 8px;
    font-size: 0.9rem;
    font-weight: 600;
    cursor: pointer;
    border: 2px solid transparent;
    transition: all 0.3s ease;
    user-select: none;
    display: inline-block;
}

/* Botón Ver - azul */
.btn-outline-primary {
    color: #0d6efd;
    background-color: transparent;
    border-color: #0d6efd;
}

.btn-outline-primary:hover,
.btn-outline-primary:focus {
    color: white;
    background-color: #0d6efd;
    border-color: #0b5ed7;
    box-shadow: 0 0 8px rgba(13, 110, 253, 0.5);
}

/* Botón Eliminar - rojo */
.btn-outline-danger {
    color: #dc3545;
    background-color: transparent;
    border-color: #dc3545;
}

.btn-outline-danger:hover,
.btn-outline-danger:focus {
    color: white;
    background-color: #dc3545;
    border-color: #b02a37;
    box-shadow: 0 0 8px rgba(220, 53, 69, 0.5);
}

/* Botones de paginación - gris */
.btn-outline-secondary {
    color: #6c757d;
    background-color: transparent;
    border-color: #6c757d;
}

.btn-outline-secondary:hover,
.btn-outline-secondary:focus {
    color: white;
    background-color: #6c757d;
    border-color: #5a6268;
    box-shadow: 0 0 8px rgba(108, 117, 125, 0.5);
}

/* Tamaño pequeño */
.btn-sm {
    padding: 5px 10px;
    font-size: 0.85rem;
}

/* Separación entre botones en la misma celda */
td .btn {
    margin: 2px 4px;
}

        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
        <h1 class="title">Cotizaciones</h1>
    </div>

    <asp:Label ID="LblidUsuario" runat="server" Text="Label" Visible="false"></asp:Label>

    <div class="table-container mt-4">
        <asp:Repeater ID="rptCotizaciones" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Nombres</th>
                            <th>Correo</th>
                            <th>Dirección</th>
                            <th>Producto</th>
                            <th>Cantidad</th>
                            <th>Observaciones</th>
                            <th>Fecha</th>
                            <th>Estado</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr data-id='<%# Eval("idCotizacion") %>'>
                    <td data-label="Nombre"><%# Eval("NombreCliente") %></td>
                    <td data-label="Correo"><%# Eval("email") %></td>
                    <td data-label="Dirección"><%# Eval("DireccionInstalacion") %></td>
                    <td data-label="Producto"><%# Eval("nombreProducto") %></td>
                    <td data-label="Cantidad"><%# Eval("Cantidad") %></td>
                    <td data-label="Cantidad"><%# Eval("Observaciones") %></td>
                    <td data-label="Fecha"><%# Eval("fechaCotizacion") %></td>
                    <td data-label="Estado"><span class="badge bg-warning text-dark"><%# Eval("Estado") %></span></td>
                    <td data-label="Acción">
                        <asp:Button ID="btnVerCotizacion" runat="server" CssClass="btn btn-outline-primary btn-sm" Text="👁 Ver Cotizacion" CommandArgument='<%# Eval("idCotizacion") %>' OnCommand="btnVerCotizacion_Command" />
                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-outline-danger btn-sm" Text="🗑 Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("idCotizacion") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div style="margin-top: 10px; text-align:center;">
            <asp:Button ID="btnPrev" runat="server" Text="« Anterior" OnClick="btnPrev_Click" CssClass="btn btn-outline-secondary btn-sm" />
            <asp:Label ID="lblPageInfo" runat="server" Text="" style="margin: 0 15px;"></asp:Label>
            <asp:Button ID="btnNext" runat="server" Text="Siguiente »" OnClick="btnNext_Click" CssClass="btn btn-outline-secondary btn-sm" />
        </div>
    </div>

</asp:Content>
