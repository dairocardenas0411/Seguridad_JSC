<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="UsuarioCotizacion.aspx.cs" Inherits="Seguridad_JSC.Vista.UsuarioCotizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="..\Vista\css\Cotizacionprint.css" rel="stylesheet" />

    <style>
        /* Estilos para ocultar sección del proveedor en la impresión */
        @media print {
            .no-print {
                display: none !important;
            }

            .seccion-proveedor {
                display: none !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMensaje" Visible="false" runat="server" ForeColor="Red" CssClass="mensaje-error"></asp:Label>
    <asp:Label ID="LblidUsuario" runat="server" Text="Label" Visible="false"></asp:Label>


    <div class="btn-container no-print" style="margin-bottom: 20px;">
        <asp:Repeater ID="rptBtn" runat="server">
            <ItemTemplate>
                <asp:Button
                    ID="btnAceptar"
                    runat="server"
                    Text="✅ Aceptar"
                    CssClass="btn-acpetar"
                    CommandArgument='<%# Eval("idCotizacion") %>'
                    OnCommand="btnAceptar_Command"
                    Visible='<%# Eval("estado").ToString() == "Pendiente" && !EsTecnico %>' />


                <asp:Button ID="bntEliminar" runat="server" Text="🗑 Eliminar"
                    CssClass="btn-eliminar" CommandName="Eliminar"
                    CommandArgument='<%# Eval("idCotizacion") %>' OnCommand="btnEliminar_Command"
                    Visible='<%# !EsTecnico %>' />

                <asp:Button ID="btnImprimir" runat="server" Text="🖨️ Imprimir"
                    CssClass="btn-imprimir" OnClientClick="imprimirSeccion(); return false;" />

                <asp:Button ID="btnAbrirModal" runat="server" Text="💱Actualizar Datos"
                    CssClass="btn-actualizar"
                    CommandArgument='<%# Eval("idCotizacion") %>'
                    OnCommand="btnAbrirModal_Command" Visible='<%# !EsTecnico %>' />
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="areaImprimir" class="cotizacion-wrapper">

        <asp:Repeater ID="rptDatosCotizacion" runat="server">
            <ItemTemplate>
                <div class="card-cotizacion">
                    <div class="section-title">
                        <i class="fas fa-user"></i>Información del Cliente
                    </div>
                    <div class="info-grid">
                        <div class="info-item">
                            <strong>Documento N°:</strong>
                            <span>#000<%# Eval("idCotizacion") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Nombre Completo:</strong>
                            <span><%# Eval("nombreCliente") %> <%# Eval("apellidoCliente") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Documento:</strong>
                            <span><%# Eval("documento") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Teléfono:</strong>
                            <span><%# Eval("telefono") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Email:</strong>
                            <span><%# Eval("email") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Dirección de Instalación:</strong>
                            <span><%# Eval("direccionInstalacion") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Tipo Trabajo:</strong>
                            <span><%# Eval("tipoTrabajo") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Fecha de Solicitud:</strong>
                            <span><%# Eval("fechaCotizacion") %></span>
                        </div>
                        <div class="info-item" style="grid-column: 1 / -1;">
                            <strong>Observaciones del Cliente:</strong>
                            <span style="white-space: pre-wrap;"><%# Eval("observaciones") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Cargos Adicionales:</strong>
                            <span>$<%# Eval("cargosAdicionales", "{0:N2}") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Valor Instalación:</strong>
                            <span>$<%# Eval("valorInstalacion", "{0:N2}") %></span>
                        </div>
                        <div class="info-item valor-destacado">
                            <strong>Valor Total del Trabajo:</strong>
                            <span>$<%# Eval("valorTotal", "{0:N2}") %></span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rptDatosProductos" runat="server">
            <ItemTemplate>
                <div class="card-cotizacion">
                    <div class="section-title">
                        <i class="fas fa-box"></i>Información del Producto
                    </div>
                    <div class="info-grid">
                        <div class="info-item">
                            <strong>Categoría:</strong>
                            <span><%# Eval("nombreCategoria") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Producto:</strong>
                            <span><%# Eval("nombreProducto") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Código:</strong>
                            <span><%# Eval("codigo") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Cantidad Solicitada:</strong>
                            <span><%# Eval("cantidad") %> unidades</span>
                        </div>
                        <div class="info-item">
                            <strong>Detalles del Producto:</strong>
                            <span><%# Eval("descripcionProducto") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Precio Unitario:</strong>
                            <span>$<%# Eval("precioUnitario", "{0:N2}") %></span>
                        </div>
                        <div class="info-item valor-destacado">
                            <strong>Valor Total Productos:</strong>
                            <span>$<%# Eval("valorProducto", "{0:N2}") %></span>
                        </div>
                    </div>

                    <div class="seccion-proveedor no-print">
                        <h4 class="subsection-title">
                            <i class="fas fa-user-cog"></i>Información del Proveedor
                        </h4>
                        <div class="info-grid">
                            <div class="info-item">
                                <strong>Cantidad Disponible:</strong>
                                <span><%# Eval("cantidadStock") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Empresa:</strong>
                                <span><%# Eval("empresa") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Proveedor:</strong>
                                <span><%# Eval("nombreProveedor") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Documento:</strong>
                                <span><%# Eval("documento") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Teléfono:</strong>
                                <span><%# Eval("celular") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Email Proveedor:</strong>
                                <span><%# Eval("email") %></span>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rptDatosProveedor" runat="server">
            <ItemTemplate>
                <div class="card-cotizacion no-print">
                    <div class="section-title">
                        <i class="fas fa-tools"></i>Información de Instalación
                    </div>

                    <div class="bloque-seccion">
                        <h4 class="subsection-title">
                            <i class="fas fa-user-hard-hat"></i>Técnico Asignado
                        </h4>
                        <div class="info-grid">
                            <div class="info-item">
                                <strong>Técnico:</strong>
                                <span><%# Eval("nombreTecnico") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Documento:</strong>
                                <span><%# Eval("documentoTecnico") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Email:</strong>
                                <span><%# Eval("emailTecnico") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Contacto:</strong>
                                <span><%# Eval("celular") %></span>
                            </div>
                            <div class="info-item valor-destacado">
                                <strong>Estado:</strong>
                                <span class="estado-badge"><%# Eval("estado") %></span>
                            </div>
                        </div>
                    </div>

                    <div class="bloque-seccion">
                        <h4 class="subsection-title">
                            <i class="fas fa-file-invoice-dollar"></i>Costos Adicionales
                        </h4>
                        <div class="info-grid">
                            <div class="info-item">
                                <strong>Cargos Adicionales:</strong>
                                <span>$<%# Eval("cargosAdicionales", "{0:N2}") %></span>
                            </div>
                            <div class="info-item">
                                <strong>Valor Instalación:</strong>
                                <span>$<%# Eval("valorInstalacion", "{0:N2}") %></span>
                            </div>
                            <div class="info-item valor-destacado">
                                <strong>Valor Total del Trabajo:</strong>
                                <span>$<%# Eval("valorTotal", "{0:N2}") %></span>
                            </div>
                            <div class="info-item" style="grid-column: 1 / -1;">
                                <strong>Observaciones del Trabajo:</strong>
                                <span style="white-space: pre-wrap;"><%# Eval("observacionesTrabajo") %></span>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rptDatosCotizacion2" runat="server">
            <ItemTemplate>
                <div class="card-cotizacion">
                    <div class="section-title">
                        <i class="fas fa-calculator"></i>Resumen Financiero
                    </div>
                    <div class="info-grid">
                        <div class="info-item">
                            <strong>Valor Total Productos:</strong>
                            <span>$<%# Eval("valorTotalProductos", "{0:N2}") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Valor Instalación:</strong>
                            <span>$<%# Eval("valorInstalacion", "{0:N2}") %></span>
                        </div>
                        <div class="info-item">
                            <strong>Cargos Adicionales:</strong>
                            <span>$<%# Eval("cargosAdicionales", "{0:N2}") %></span>
                        </div>
                        <div class="info-item valor-destacado" style="grid-column: 1 / -1; font-size: 1.2em;">
                            <strong>VALOR TOTAL FINAL:</strong>
                            <span>$<%# Eval("valorTotal", "{0:N2}") %></span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="modal fade no-print" id="modalActualizar" tabindex="-1" aria-labelledby="modalActualizarLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalActualizarLabel">Actualizar Datos de Cotización</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hfIdTrabajo" runat="server" />

                    <h6 class="mb-3"><i class="fas fa-user"></i>Datos del Cliente</h6>
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <label for="txtNombreCliente" class="form-label">Nombre Cliente</label>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtApellidoCliente" class="form-label">Apellido Cliente</label>
                            <asp:TextBox ID="txtApellidoCliente" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtDocumento" class="form-label">Documento</label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtTelefono" class="form-label">Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtDireccion" class="form-label">Dirección de Instalación</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-12">
                            <label for="txtObservaciones" class="form-label">Observaciones Cliente</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                        </div>
                    </div>

                    <h6 class="mb-3"><i class="fas fa-box"></i>Datos del Producto</h6>
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <label for="ddlListaProductos" class="form-label">Tipo de Producto</label>
                            <asp:DropDownList ID="ddlListaProductos" runat="server" CssClass="form-select" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtCantidad" class="form-label">Cantidad Solicitada</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-md-6">
                            <label for="ddlTipoTrabajo" class="form-label">Tipo Trabajo</label>
                            <asp:DropDownList ID="ddlTipoTrabajo" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Cotización" Value="Cotizacion" />
                                <asp:ListItem Text="Instalación" Value="Instalacion" />
                                <asp:ListItem Text="Venta" Value="Venta" />
                                <asp:ListItem Text="Mantenimiento" Value="Mantenimiento" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label for="ddlTecnico" class="form-label">Técnico Asignado</label>
                            <asp:DropDownList ID="ddlTecnico" runat="server" CssClass="form-select" />
                        </div>
                    </div>

                    <h6 class="mb-3"><i class="fas fa-dollar-sign"></i>Costos</h6>
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <label for="txtValorInstalacion" class="form-label">Valor Instalación</label>
                            <asp:TextBox ID="txtValorInstalacion" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtCargosAdicionales" class="form-label">Cargos Adicionales</label>
                            <asp:TextBox ID="txtCargosAdicionales" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-12">
                            <label for="txtObservacionesTecnico" class="form-label">Observaciones del Trabajo</label>
                            <asp:TextBox ID="txtObservacionesTecnico" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                        </div>
                        <div class="col-12">
                            <label for="ddlEstado" class="form-label">Estado de la Cotización</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                                <asp:ListItem Text="Aceptada" Value="Aceptada" Enabled="false" Style="display: none;"></asp:ListItem>
                                <asp:ListItem Text="Rechazada" Value="Rechazada"></asp:ListItem>
                                <asp:ListItem Text="En Instalación" Value="En Instalacion"></asp:ListItem>
                                <asp:ListItem Text="En Revisión" Value="En Revision"></asp:ListItem>
                                <asp:ListItem Text="Técnico Completada" Value="Tecnico Completada"></asp:ListItem>
                                <asp:ListItem Text="Completada" Value="Completada"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="bntActualizar" runat="server" Text="💾 Actualizar Cotización"
                        CssClass="btn-acpetar" CommandName="Actualizar"
                        OnCommand="btnActualizar_Command" />
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script src="..\Vista\js\JavaScript.js"></script>

</asp:Content>
