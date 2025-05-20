using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClLoginL
	{
		private ClLoginD usuarioDatos = new ClLoginD();

		public ClLoginE MtdIngresoL(ClLoginE oUsuarioDatos)
		{
			try
			{
				ClLoginD oUsuarioD = new ClLoginD();
				ClLoginE oUsuario = oUsuarioD.MtdIngresoUsuarios(oUsuarioDatos);
				return oUsuario;
			}
			catch(Exception ex)
			{
				throw new Exception("Error en logica login", ex);
			}
		}
	}
}