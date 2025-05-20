using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Entidades
{
	public class ClLoginE
	{
        public int idUsuario { get; set; }
        public int documento { get; set; }
        public string nombreUsuario { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        public int idRol { get; set; }
    }
}