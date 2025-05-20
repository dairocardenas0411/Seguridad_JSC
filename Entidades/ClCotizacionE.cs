
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClCotizacionE
	{
		public int idCotizacion { get; set; }
		public string nombreCliente { get; set; }
		public string apellidoCliente { get; set; }
		public int documento { get; set; }

        public string telefono{ get; set; }
		public string email { get; set; }
		public string direccionInstalacion { get; set; }
		public string estado { get; set; }
		public int idProducto { get; set; }
		public int cantidad { get; set; }
		public DateTime fechaCotizacion { get; set; }
        public string observaciones { get; set; }
		public string nombreProducto { get; set; }
        public bool validacion { get; set; }

    }
}