
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{

    public class ClCotizacionE
    {

        public int idCotizacion { get; set; }
        public int? idUsuarioT { get; set; }

        public string estado { get; set; }
        public double valorProducto { get; set; }
        public double valorTotal { get; set; }

        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public int idProducto { get; set; }
        public DateTime fechaCotizacion { get; set; }
        public bool validacion { get; set; }
        // Propiedades básicas del cliente
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public int documento { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string direccionInstalacion { get; set; }

        // Propiedades de la cotización
        public string observaciones { get; set; }
        public decimal valorInstalacion { get; set; }
        public decimal cargosAdicionales { get; set; }
        public string tipoTrabajo { get; set; }
        public string observacionesTrabajo { get; set; }
        public int? idUsuario { get; set; }
        public decimal valorProductoA { get; set; } 

        // Lista de productos para la cotización
        public List<ProductoCotizacion> Productos { get; set; }

      
    }
    [Serializable]
    public class ProductoCotizacion
    {
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public decimal valorProductoA { get; set; }
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal valorProducto { get; set; }



    }

}