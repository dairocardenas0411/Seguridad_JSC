using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClListaProductoL
	{
        public List<ClProductoE> MtdListarProducto()
        {
            ClListaProductoD oDatos = new ClListaProductoD();
            List<ClProductoE> ListaProductos= oDatos.MtdListarProducto();
            return ListaProductos;
        }
    }
}