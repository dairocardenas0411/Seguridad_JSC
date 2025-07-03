function confirmarEliminacion(idCotizacion) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: '¡Esta Acción Eliminará los Datos Permanentemente!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar 🗑',
        cancelButtonText: 'Cancelar',
        customClass: {
            confirmButton: 'swal2-confirm btn btn-danger btn-lg rounded-pill px-4 me-3',
            cancelButton: 'swal2-cancel btn btn-secondary btn-lg rounded-pill px-4'
        },
        buttonsStyling: false
    }).then((result) => {
        if (result.isConfirmed) {
            __doPostBack('EliminarCotizacion' + idCotizacion, '');
        }
    });
    return false;
}


// Evita que Enter en inputs recargue la página
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('input').forEach(function (input) {
        input.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault();
                return false;
            }
        });
    });
});


// filtrar tabla
function filtrarTabla() {
    var input = document.getElementById("searchInput");
    var filter = input.value.toLowerCase();
    var table = document.getElementById("tablaCotizaciones");
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
    actualizarIconoBusqueda(filter);
}

// Función para mostrar mensaje de búsqueda
function mostrarMensajeBusqueda(count, filter) {
    var existingMsg = document.getElementById("noResultsMessage");
    if (existingMsg) {
        existingMsg.remove();
    }

    if (count === 0 && filter.length > 0) {
        var tableContainer = document.querySelector(".table-container");
        var message = document.createElement("div");
        message.id = "noResultsMessage";
        message.className = "no-results";
        message.innerHTML = `
                    <i class="fas fa-search"></i>
                    <h3>No se encontraron resultados</h3>
                    <p>No hay Resultados que coincidan con "${filter}"</p>
                    <small>Intenta con otros términos de búsqueda</small>
                `;
        tableContainer.appendChild(message);
    }
}
// Función para actualizar icono de búsqueda
function actualizarIconoBusqueda(filter) {
    var icon = document.getElementById('searchIcon');
    if (filter.length > 0) {
        icon.className = 'fas fa-times';
        icon.style.cursor = 'pointer';
        icon.style.color = '#e74c3c';
        icon.onclick = function () {
            document.getElementById('searchInput').value = '';
            filtrarTabla();
        };
    } else {
        icon.className = 'fas fa-search';
        icon.style.cursor = 'default';
        icon.style.color = '#667eea';
        icon.onclick = null;
    }
}




// Impirmir Documento
function imprimirSeccion() {
    var areaCompleta = document.getElementById('areaImprimir');
    var contenidoImprimir = areaCompleta.outerHTML;
    var ventanaImpresion = window.open('', '_blank', 'width=900,height=700');
    ventanaImpresion.document.write(`
<!DOCTYPE html>
<html>
<head>
    <title>Factura</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        
        /* REGLA IMPORTANTE: Ocultar elementos no-print */
        .no-print {
            display: none !important;
            visibility: hidden !important;
        }
        
        body {
            font-family: 'Inter', sans-serif;
            padding: 40px;
            background: #fff;
            color: #333;
        }
        .contenedor {
            max-width: 900px;
            margin: auto;
        }
        .encabezado-impresion {
            text-align: center;
            border-bottom: 3px solid #444;
            padding-bottom: 15px;
            margin-bottom: 30px;
        }
        .encabezado-impresion h1 {
            font-size: 2.2rem;
            margin-bottom: 5px;
        }
        .card-cotizacion {
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 25px;
            margin-bottom: 30px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.08);
        }
        .section-title {
            font-weight: 600;
            font-size: 1.2rem;
            margin-bottom: 15px;
            display: flex;
            align-items: center;
            gap: 8px;
            color: #444;
            border-left: 5px solid #007bff;
            padding-left: 10px;
        }
        .info-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 12px;
            margin-top: 15px;
        }
        .info-item {
            font-size: 0.95rem;
        }
        .info-item span {
            display: block;
            color: #222;
            margin-top: 3px;
            font-weight: 500;
        }
        .valor-destacado {
            font-size: 1rem;
            background: #f2f8ff;
            padding: 10px;
            border-left: 4px solid #007bff;
            border-radius: 6px;
        }
        .estado-badge {
            padding: 4px 10px;
            background: #007bff;
            color: white;
            border-radius: 12px;
            display: inline-block;
            font-size: 0.85rem;
        }
        .pie-impresion {
            text-align: center;
            margin-top: 40px;
            padding-top: 20px;
            border-top: 1px solid #ddd;
            color: #666;
            font-size: 0.85rem;
        }
        @media print {
            body { padding: 0; margin: 0; }
            .contenedor { padding: 0; }
            .no-print {
                display: none !important;
                visibility: hidden !important;
            }
        }
    </style>
</head>
<body>
    <div class="contenedor">
        <div class="encabezado-impresion">
            <h1>📋 TRABAJO</h1>
        </div>
        ${contenidoImprimir}
        <div class="pie-impresion">
            <p>Documento generado el: ${new Date().toLocaleDateString('es-ES', {
        year: 'numeric', month: 'long', day: 'numeric',
        hour: '2-digit', minute: '2-digit'
    })}</p>
        </div>
    </div>
</body>
</html>
            `);
    ventanaImpresion.document.close();
    setTimeout(function () {
        ventanaImpresion.focus();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }, 500);
}