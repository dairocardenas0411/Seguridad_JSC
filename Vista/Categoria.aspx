<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="Seguridad_JSC.Vista.Categoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Vista/css/Categoria.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ScriptManager necesario para UpdatePanel -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="categoria-container fade-in">
        <!-- Header -->
        <div class="page-header">
            <div class="header-content">
                <div class="header-info">
                    <h1 class="page-title"><i class="fas fa-tags"></i>Gestión de Categorías</h1>
                    <p class="page-subtitle">Administra las categorías de productos del sistema</p>
                </div>
                <div class="header-actions">
                    <asp:Button ID="btnAbrirModal" runat="server"
                        Text="Nueva Categoría"
                        CssClass="btn btn-primary btn-create"
                        OnClick="btnAbrirModal_Click"
                        OnClientClick="$('#modalCategoria').modal('show'); return false;" />
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="modalCategoria" tabindex="-1" role="dialog" aria-labelledby="modalCategoriaLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-title-container">
                            <i class="fas fa-plus-circle modal-icon"></i>
                            <h5 class="modal-title" id="modalCategoriaLabel">Nueva Categoría</h5>
                        </div>
                        <button type="button" class="close-btn" data-dismiss="modal" aria-label="Close">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!-- Formulario -->
                        <div class="form-container">
                            <div class="input-group-enhanced">
                                <div class="input-icon">
                                    <i class="fas fa-tag"></i>
                                </div>
                                <div class="input-field">
                                    <label for="<%= txtNombreCategoria.ClientID %>" class="floating-label">Nombre de la Categoría *</label>
                                    <asp:TextBox ID="txtNombreCategoria" runat="server"
                                        CssClass="form-control-enhanced"
                                        placeholder="Ingrese el nombre de la categoría"
                                        MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombreCategoria" runat="server"
                                        ControlToValidate="txtNombreCategoria"
                                        ErrorMessage="El nombre es requerido"
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ValidationGroup="CategoriaGroup" />
                                </div>
                            </div>

                            <div class="input-group-enhanced">
                                <div class="input-icon">
                                    <i class="fas fa-align-left"></i>
                                </div>
                                <div class="input-field">
                                    <label for="<%= txtDescripcion.ClientID %>" class="floating-label">Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server"
                                        CssClass="form-control-enhanced textarea-enhanced"
                                        TextMode="MultiLine"
                                        placeholder="Descripción detallada de la categoría"
                                        MaxLength="255"
                                        Rows="4"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="button-group">
                            <asp:Button ID="btnGuardarCategoria" runat="server" style="color:white"
                                CssClass="btn btn-save"
                                Text="Guardar Categoría"
                                OnClick="btnGuardarCategoria_Click"
                                ValidationGroup="CategoriaGroup" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- GridView con UpdatePanel -->
        <div class="grid-container">
            <div class="grid-header">
                <div class="grid-title">
                    <h5><i class="fas fa-list"></i>Lista de Categorías</h5>
                </div>
            </div>

            <div class="table-wrapper">
                <asp:UpdatePanel ID="UpdatePanelCategorias" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCategorias" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table-enhanced"
                            DataKeyNames="idCategoria"
                            OnRowEditing="gvCategorias_RowEditing"
                            OnRowCancelingEdit="gvCategorias_RowCancelingEdit"
                            OnRowUpdating="gvCategorias_RowUpdating"
                            OnRowDeleting="gvCategorias_RowDeleting"
                            EmptyDataText="No hay categorías registradas">

                            <HeaderStyle CssClass="table-header" />
                            <RowStyle CssClass="table-row" />
                            <AlternatingRowStyle CssClass="table-row-alt" />
                            <EditRowStyle CssClass="table-row-edit" />

                            <Columns>
                                <asp:BoundField DataField="idCategoria" HeaderText="ID" ReadOnly="True" Visible="false" />

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fas fa-tag cell-icon"></i>Nombre Categoria
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="cell-content">
                                            <span class="cell-text"><%# Eval("nombreCategoria") %></span>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditNombre" runat="server"
                                            Text='<%# Bind("nombreCategoria") %>'
                                            CssClass="edit-input"
                                            MaxLength="50" />
                                        <asp:RequiredFieldValidator ID="rfvEditNombre" runat="server"
                                            ControlToValidate="txtEditNombre"
                                            ErrorMessage="*"
                                            CssClass="text-danger"
                                            Display="Dynamic" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fas fa-align-left cell-icon"></i>Descripción
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="cell-content">
                                            <span class="cell-text description-text"><%# Eval("descripcion") %></span>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditDescripcion" runat="server"
                                            Text='<%# Bind("descripcion") %>'
                                            TextMode="MultiLine"
                                            CssClass="edit-input edit-textarea"
                                            MaxLength="255"
                                            Rows="3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="200px">
                                    <HeaderTemplate>
                                        <i class="fas fa-tools cell-icon"></i>Acciones
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:LinkButton ID="btnEdit" runat="server"
                                                CommandName="Edit"
                                                CssClass="btn-action btn-edit"
                                                ToolTip="Editar categoría"
                                                CausesValidation="false"> Editar
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnDelete" runat="server"
                                                CommandName="Delete"
                                                CssClass="btn-action btn-delete"
                                                ToolTip="Eliminar categoría"
                                                CausesValidation="false"
                                                OnClientClick="return confirmarEliminacion();">Eliminar
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="action-buttons">
                                            <asp:LinkButton ID="btnUpdate" runat="server"
                                                CommandName="Update"
                                                CssClass="btn-action btn-save-small">Guardar
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnCancel" runat="server"
                                                CommandName="Cancel"
                                                CssClass="btn-action btn-cancel-small"
                                                CausesValidation="false">Cancelar
                                            </asp:LinkButton>
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <EmptyDataTemplate>
                                <div class="empty-state">
                                    <i class="fas fa-inbox empty-icon"></i>
                                    <h4>No hay categorías registradas</h4>
                                    <p>Comienza agregando tu primera categoría</p>
                                    <button type="button" class="btn btn-primary" onclick="$('#modalCategoria').modal('show');">
                                        Agregar Categoría
                                    </button>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
