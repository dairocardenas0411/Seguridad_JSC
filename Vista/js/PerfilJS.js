
        // Preview de imagen antes de subir
    document.addEventListener('DOMContentLoaded', function () {
            const fileInput = document.querySelector('[id$="txtImagenPrincipal"]');
    const profileImage = document.querySelector('[id$="imgFoto"]');
    const uploadLabel = document.querySelector('.file-upload-label span');

    if (fileInput) {
        fileInput.addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file && file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    if (profileImage) {
                        profileImage.src = e.target.result;
                    }
                };
                reader.readAsDataURL(file);
                uploadLabel.textContent = file.name;
            }
        });
            }

        });
    function aplicarBadgesEstado() {
            // Aplica badges a las celdas de la tabla
            var estadoCells = document.querySelectorAll('#tablaTecnicos td:nth-child(6)');
    estadoCells.forEach(function (cell) {
                var estado = cell.textContent.trim().toLowerCase();
    var badge = document.createElement('span');
    badge.className = 'status-badge';

    if (estado === 'Activo') {
        badge.style.background = 'var(--success-gradient)';
    badge.style.color = 'white';
    badge.style.boxShadow = '0 2px 10px rgba(46, 204, 113, 0.3)';
                } else if (estado === 'Inactivo') {
        badge.style.background = 'var(--danger-gradient)';
    badge.style.color = 'white';
    badge.style.boxShadow = '0 2px 10px rgba(231, 76, 60, 0.3)';
                } else {
        badge.style.background = '#95a5a6';
    badge.style.color = 'white';
                }

    badge.textContent = cell.textContent;
    cell.innerHTML = '';
    cell.appendChild(badge);
            });

    // Aplica badge al Label ASP.NET lblEstado
    var lbl = document.getElementById('<%= lblEstado.ClientID %>');
    if (lbl) {
                var estadoLbl = lbl.textContent.trim().toLowerCase();
    lbl.classList.remove('status-active', 'status-inactive'); // Remueve clases previas

    if (estadoLbl === 'activo') {
        lbl.classList.add('status-badge');
    lbl.style.background = 'var(--success-gradient)';
    lbl.style.color = 'white';
    lbl.style.boxShadow = '0 2px 10px rgba(46, 204, 113, 0.3)';
                } else if (estadoLbl === 'inactivo') {
        lbl.classList.add('status-badge');
    lbl.style.background = 'var(--danger-gradient)';
    lbl.style.color = 'white';
    lbl.style.boxShadow = '0 2px 10px rgba(231, 76, 60, 0.3)';
                } else {
        lbl.classList.add('status-badge');
    lbl.style.background = '#95a5a6';
    lbl.style.color = 'white';
                }
            }
        }

    // Validación del formulario
    function validarFormulario() {
            const nombre = document.querySelector('[id$="txtNombre"]').value.trim();
    const documento = document.querySelector('[id$="txtDocumento"]').value.trim();
    const celular = document.querySelector('[id$="txtCelular"]').value.trim();
    const email = document.querySelector('[id$="txtEmail"]').value.trim();
    const password = document.querySelector('[id$="TxtPassword"]').value;

    if (!nombre) {
        Swal.fire({
            icon: 'warning',
            title: 'Campo requerido',
            text: 'El nombre es obligatorio',
            confirmButtonColor: '#667eea'
        });
    return false;
            }

    if (!documento || !/^\d+$/.test(documento)) {
        Swal.fire({
            icon: 'warning',
            title: 'Documento inválido',
            text: 'Ingrese un número de documento válido',
            confirmButtonColor: '#667eea'
        });
    return false;
            }


    if (!email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Email inválido',
            text: 'Ingrese un email válido',
            confirmButtonColor: '#667eea'
        });
    return false;
            }

    if (!password || password.length < 6) {
        Swal.fire({
            icon: 'warning',
            title: 'Contraseña muy corta',
            text: 'La contraseña debe tener al menos 6 caracteres',
            confirmButtonColor: '#667eea'
        });
    return false;
            }

    // Mostrar loading
    const card = document.querySelector('.profile-card');
    card.classList.add('loading');

    return true;
        }

    // Efectos de hover en los inputs
    document.addEventListener('DOMContentLoaded', function () {
            const inputs = document.querySelectorAll('.form-control');

            inputs.forEach(input => {
        input.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

    input.addEventListener('blur', function () {
        this.parentElement.classList.remove('focused');
                });
            });
        });
