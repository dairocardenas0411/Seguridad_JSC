using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClProveedorL
	{
        public bool MtdRegistroProveedor(ClProveedorE registro)
        {
            ClProveedorD oRegistro = new ClProveedorD();
            return oRegistro.MtdRegistroProveedor(registro);
        }
        public List<ClProveedorE> MtdListaProveedores()
        {
            ClProveedorD oDatos = new ClProveedorD();
            List<ClProveedorE> listaProveedores = oDatos.MtdListaProveedores();
            return listaProveedores;
        }
        public DataTable MtdDatosProductoProveedor(int idProveedor)
        {
            if (idProveedor <= 0)

                throw new ArgumentException("El ID del Proveedor no es válido.", nameof(idProveedor));
            ClProveedorD datosProducto = new ClProveedorD();
            return datosProducto.MtdProductosProveedor(idProveedor);

        }
        public int MtdActualizarProveedor(ClProveedorE proveedor)
        {
            ClProveedorD oDatos = new ClProveedorD();
            return oDatos.ActualizarProveedor(proveedor);
        }
    }
}





