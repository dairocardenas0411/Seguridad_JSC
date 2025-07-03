using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Logica
{
	public class ClCategoriaL
	{
		public bool MtdRegistroCategoria(ClCategoriaE oCategoria)
        {
            if (oCategoria == null)
                throw new ArgumentNullException(nameof(oCategoria), "La categoría no puede ser nula.");
            ClCategoriaD oDatos = new ClCategoriaD();
            return oDatos.MtdRegistroCategoria(oCategoria);
        }
        public List<ClCategoriaE> MtdListaCategorias()
        {
            ClCategoriaD oDatos = new ClCategoriaD();
            List<ClCategoriaE> ListaCategorias = oDatos.MtdListaCategoria();
            return ListaCategorias;
        }
        public bool MtdActualizarCategoria(ClCategoriaE categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria), "La categoría no puede ser nula.");

            ClCategoriaD oDatos = new ClCategoriaD();
            return oDatos.MtdActualizarCategoria(categoria);
        }

        public bool MtdEliminarCategoria(int idCategoria)
        {
            ClCategoriaD oDatos = new ClCategoriaD();
            return oDatos.MtdEliminarCategoria(idCategoria);
        }


    }
}


