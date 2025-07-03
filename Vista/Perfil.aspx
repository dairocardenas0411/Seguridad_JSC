<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Master.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Seguridad_JSC.Vista.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <link href="../Vista/css/Perfil.css" rel="stylesheet" />


    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Floating Background Elements -->
    <div class="floating-elements">
        <div class="floating-element"></div>
        <div class="floating-element"></div>
        <div class="floating-element"></div>
    </div>

    <div class="profile-container">

        <!-- Profile Card -->
        <div class="profile-card">
            <asp:HiddenField ID="hfIdUsuario" runat="server" />

            <!-- Profile Image Section -->
            <div class="profile-image-section">
                <div class="profile-image-wrapper">
                    <asp:Image ID="imgFoto" runat="server" CssClass="profile-image" AlternateText="Foto de Perfil" />
                    <div class="image-overlay">
                        <i class="fas fa-camera"></i>
                    </div>
                </div>

                <asp:HiddenField ID="hfNombreImagen" runat="server" />

                <div class="form-group">
                    <label for="txtImagenPrincipal">
                        <i class="fas fa-image"></i>Cambiar Foto de Perfil
                    </label>
                    <div class="file-upload-wrapper">
                        <asp:FileUpload ID="txtImagenPrincipal" runat="server" CssClass="file-upload-input" accept="image/*" />
                        <div class="file-upload-label">
                            <i class="fas fa-cloud-upload-alt"></i>
                            <span>Seleccionar imagen (JPG, PNG máx. 5MB)</span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Form Grid -->
            <div class="form-grid">
                <!-- Left Column -->
                <div class="form-column">
                    <div class="form-group">
                        <label for="txtNombre">
                            <i class="fas fa-user"></i>Nombre Completo
                        </label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                            placeholder="Ingresa tu nombre completo" MaxLength="100" />
                    </div>

                    <div class="form-group">
                        <label for="txtDocumento">
                            <i class="fas fa-id-card"></i>Documento de Identidad
                        </label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control"
                            placeholder="Número de documento" MaxLength="20" />
                    </div>

                    <div class="form-group">
                        <label for="txtCelular">
                            <i class="fas fa-mobile-alt"></i>Número de Celular
                        </label>
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"
                            placeholder="Ej: 3001234567" MaxLength="15" />
                    </div>
                </div>

                <!-- Right Column -->
                <div class="form-column">
                    <div class="form-group">
                        <label for="txtEmail">
                            <i class="fas fa-envelope"></i>Correo Electrónico
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                            placeholder="tu@email.com" TextMode="Email" MaxLength="100" />
                    </div>

                    <div class="form-group">
                        <label for="TxtPassword">
                            <i class="fas fa-lock"></i>Nueva Contraseña
                        </label>
                        <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control"
                            placeholder="Mínimo 6 caracteres" TextMode="Password" MaxLength="50" />
                    </div>

                    <div class="form-group">
                        <label for="lblEstado">
                            <i class="fas fa-info-circle"></i>Estado de la Cuenta
                        </label>
                        <asp:Label ID="lblEstado" runat="server" CssClass="status-badge status-active" />
                    </div>
                </div>
            </div>

            <!-- Update Button -->
            <div class="form-group">
                <asp:Button ID="bntActualizar" runat="server"
                    Text="Actualizar Perfil"
                    CssClass="btn-actualizar"
                    CommandName="Actualizar"
                    OnCommand="bntActualizar_Command"
                    OnClientClick="return validarFormulario();" />
            </div>
        </div>
    </div>

     <script src="..\Vista\js\PerfilJS.js"></script>
</asp:Content>
