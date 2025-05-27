using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
    public class ClCotizacionL
    {

        public int MtdRegistroCotizacion(ClCotizacionE oCotizacion)
        {
            if (oCotizacion == null)
                throw new ArgumentNullException(nameof(oCotizacion), "La cotización no puede ser nula.");

            ClCotizacionD oDatos = new ClCotizacionD();
            return oDatos.MtdRegistroCotizacion(oCotizacion);
        }




        public List<ClCotizacionE> MtdListarCotizacionPendiente()
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            List<ClCotizacionE> listaCotizacion = oDatos.MtdListadCotizacionPendiente();
            return listaCotizacion;
        }


        public DataTable MtdDatosProductos(int idCotizacion)
        {
            if (idCotizacion <= 0)

                throw new ArgumentException("El ID del Trabajo no es válido.", nameof(idCotizacion));
            ClCotizacionD datosProducto = new ClCotizacionD();
            return datosProducto.MtdInfoProducto(idCotizacion);

        }
        public DataTable ObtenerDatosCotizacion(int idCotizacion)
        {
            if (idCotizacion <= 0)
                throw new ArgumentException("El ID de cotización no es válido.", nameof(idCotizacion));

            ClCotizacionD datosCotizacion = new ClCotizacionD();

            return datosCotizacion.MtdInfoCotizacion(idCotizacion);

        }

        public bool MtdActualizarTrabajo(ClCotizacionE trabajo)
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            bool resultado = oDatos.MtdActualizarTrabajo(trabajo);
            return resultado;
        }
        public DataTable MtdListaTrabajoTecnico(int idUsuarioT)
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            return oDatos.MtdListaTrabajoTecnico(idUsuarioT);
        }


    }
}