using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClUsuarioL
	{
        public List<ClUsuarioE> MtdListaTecnicos()
        {
            ClUsuarioD oDatos = new ClUsuarioD();
            List<ClUsuarioE> ListaTecnicos = oDatos.MtdListaTecnicos();
            return ListaTecnicos;
        }
    }
}