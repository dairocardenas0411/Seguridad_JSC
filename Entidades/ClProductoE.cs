using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClProductoE
	{
		public int idProducto { get; set; }
		public string codigo { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
		public decimal precioUnitario { get; set; }
        public int cantidadstock { get; set; }
		public string estado { get; set; }
        public string imagen { get; set; }
		public int idCategoria { get; set; }
		public int idProveedor { get; set; }
		public int idUsuario { get; set; }
		public bool validacion { get; set; }

    }
}