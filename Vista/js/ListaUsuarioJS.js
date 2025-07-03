
    function filtrarTabla() {
            var input = document.getElementById("searchInput");
    var filter = input.value.toLowerCase();
    var table = document.getElementById("tablaTecnicos");
    var trs = table.getElementsByTagName("tr");
    var visibleCount = 0;

    for (var i = 1; i < trs.length; i++) {
                var tds = trs[i].getElementsByTagName("td");
    var mostrar = false;

    for (var j = 0; j < tds.length; j++) {
                    if (tds[j].textContent.toLowerCase().indexOf(filter) > -1) {
        mostrar = true;
    break;
                    }
                }

    if (mostrar) {
        trs[i].style.display = "";
    visibleCount++;
    // Animación de entrada
    trs[i].style.opacity = "0";
    trs[i].style.transform = "translateY(20px)";
    setTimeout(function (row) {
                        return function () {
        row.style.transition = "all 0.3s ease";
    row.style.opacity = "1";
    row.style.transform = "translateY(0)";
                        };
                    }(trs[i]), i * 50);
                } else {
        trs[i].style.display = "none";
                }
            }

    // Mostrar mensaje si no hay resultados
    mostrarMensajeBusqueda(visibleCount, filter);
        }

    function mostrarMensajeBusqueda(count, filter) {
            var existingMsg = document.getElementById("noResultsMessage");
    if (existingMsg) {
        existingMsg.remove();
            }

            if (count === 0 && filter.length > 0) {
                var tableContainer = document.querySelector(".table-container");
    var message = document.createElement("div");
    message.id = "noResultsMessage";
    message.innerHTML = `
    <div style="text-align: center; padding: 40px; color: #7f8c8d;">
        <i class="fas fa-search" style="font-size: 48px; margin-bottom: 20px; opacity: 0.5;"></i>
        <h3 style="margin-bottom: 10px;">No se encontraron resultados</h3>
        <p>No hay técnicos que coincidan con "${filter}"</p>
    </div>
    `;
    tableContainer.appendChild(message);
            }
        }

    // Función para crear badges de estado dinámicamente
    function aplicarBadgesEstado() {
            var estadoCells = document.querySelectorAll('#tablaTecnicos td:nth-child(6)');
    estadoCells.forEach(function (cell) {
                var estado = cell.textContent.trim().toLowerCase();
    var badge = document.createElement('span');
    badge.className = 'status-badge';

    if (estado === 'activo') {
        badge.style.background = 'var(--success-gradient)';
    badge.style.color = 'white';
    badge.style.boxShadow = '0 2px 10px rgba(46, 204, 113, 0.3)';
                } else if (estado === 'inactivo') {
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
        }



    // Función para animación de filas
    function animarFilasTabla() {
            var rows = document.querySelectorAll('#tablaTecnicos tbody tr');
    rows.forEach(function (row, index) {
        row.style.opacity = '0';
    row.style.transform = 'translateY(20px)';
    setTimeout(function () {
        row.style.transition = 'all 0.3s ease';
    row.style.opacity = '1';
    row.style.transform = 'translateY(0)';
                }, index * 100);
            });
        }

    // Función para mejorar la experiencia de usuario
    function mejorarUX() {
            // Añadir efecto de loading al botón aplicar
            var btnAplicar = document.getElementById('btnAplicarNumero');
    if (btnAplicar) {
        btnAplicar.addEventListener('click', function () {
            this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Aplicando...';
            this.disabled = true;

            setTimeout(function () {
                btnAplicar.innerHTML = '<i class="fas fa-check"></i> Aplicar';
                btnAplicar.disabled = false;
            }, 1000);
        });
            }

    // Mejorar accesibilidad del input de búsqueda
    var searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var icon = document.getElementById('searchIcon');
            if (this.value.length > 0) {
                icon.className = 'fas fa-times';
                icon.style.cursor = 'pointer';
                icon.onclick = function () {
                    searchInput.value = '';
                    filtrarTabla();
                    this.className = 'fas fa-search';
                    this.style.cursor = 'default';
                    this.onclick = null;
                };
            } else {
                icon.className = 'fas fa-search';
                icon.style.cursor = 'default';
                icon.onclick = null;
            }
        });
            }
        }

    // Inicialización cuando el DOM esté listo
    document.addEventListener('DOMContentLoaded', function () {
        aplicarBadgesEstado();
    inicializarPlaceholderDinamico();
    animarFilasTabla();
    mejorarUX();
        });

    // También ejecutar después de postbacks de ASP.NET
    function pageLoad() {
        aplicarBadgesEstado();
    animarFilasTabla();
    mejorarUX();

    // Restaurar el placeholder del input de búsqueda
    var searchInput = document.getElementById('searchInput');
    if (searchInput && !searchInput.placeholder) {
        searchInput.placeholder = 'Buscar por nombre, correo, documento o teléfono...';
            }
        }

    // Para ASP.NET UpdatePanel compatibility
    if (typeof Sys !== 'undefined') {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pageLoad);
        }
