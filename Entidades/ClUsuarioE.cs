using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClUsuarioE
	{
		public int IdUsuario { get; set; }
		public int documento { get; set; }
        public string nombreUsuario { get; set; }
		public string cellular { get; set; }
		public string estado { get; set; }
        public string email { get; set; }
		public string contraseña { get; set; }
		public int idRol { get; set; }
		public string nombreRol { get; set; }
        public bool validacion { get; set; }
    }
}