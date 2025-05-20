using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClCotizacionL
	{
        public bool MtdRegistroCotizacion(ClCotizacionE oCotizacion)
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            bool resultado = oDatos.MtdRegistroCotizacion(oCotizacion);
            return resultado;
        }

        public List<ClCotizacionE> MtdListarCotizacionPendiente()
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            List<ClCotizacionE> listaCotizacion = oDatos.MtdListadCotizacionPendiente();
            return listaCotizacion;
        }
    }
}