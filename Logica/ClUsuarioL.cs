using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool MtdRegistroUsuario(ClUsuarioE registro)
        {
            if (MtdVerificarDocumento(registro.documento))
            {
                return false;
            }

            if (MtdVerificarCorreo(registro.email))
            {
                return false;
            }

            ClUsuarioD oRegistro = new ClUsuarioD();
            return oRegistro.MtdRegistroUsuario(registro);
        }

        public bool MtdVerificarDocumento(int documento)
        {

            ClUsuarioD oRegistro = new ClUsuarioD();
            return oRegistro.VerificarDocumento(documento);
        }

        public bool MtdVerificarCorreo(string correo)
        {

            ClUsuarioD oRegistro = new ClUsuarioD();
            return oRegistro.VerificarCorreo(correo);
        }

        public DataTable MtdDatosPerfil(int idUsuario)
        {
            if (idUsuario <= 0)

                throw new ArgumentException("El ID del Usuario no es válido.", nameof(idUsuario));
            ClUsuarioD datosUsuario = new ClUsuarioD();
            return datosUsuario.MtdPerfil(idUsuario);

        }

        public bool MtdActualizarUsuario(ClUsuarioE Usuario)
        {
            ClUsuarioD oDatos = new ClUsuarioD();
            bool resultado = oDatos.MtdActualizarUsuario(Usuario);
            return resultado;
        }
    }
}