using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClProveedorE
	{
		public int idProveedor {  get; set; }
		public string nombre { get; set; }
		public int documento { get; set; }
		public string celular {  get; set; }
		public string empresa { get; set; }
		public string email {  get; set; }
		public string imagen { get; set; }
		public bool validacion {  get; set; }
		public string TotalProductos { get; set; }

    }
}