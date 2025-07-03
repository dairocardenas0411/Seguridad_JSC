using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClRolL
	{
        public List<ClRolE> MtdListarRoles()
        {
            ClRolD oDatos = new ClRolD();
            List<ClRolE> ListaRoles = oDatos.MtdListaRol();
            return ListaRoles;
        }
    }
}