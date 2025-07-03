using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClCategoriaE
	{
		public int idCategoria { get; set; }
		public string nombreCategoria { get; set; }
		public string descripcion { get; set; }
		public bool validacion { get;set; }
	}
}