using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public List<ClCotizacionE> MtdListarTrabajo()
        {
            ClCotizacionD oDatos = new ClCotizacionD();
            List<ClCotizacionE> listaTrabajo = oDatos.MtdListaTrabajoProgreso();
            return listaTrabajo;
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
        public bool MtdActualizarProductosCotizacion(int idCotizacion, List<ProductoCotizacion> productos)
        {
            if (productos == null || productos.Count == 0)
            {
                // No hacer nada si no hay productos nuevos
                return false;
            }

            ClConexion conexion = new ClConexion();
            SqlConnection con = conexion.MtdAbrirConexion();
            SqlTransaction transaccion = con.BeginTransaction();

            try
            {
                // Primero eliminar todos los productos actuales de esa cotización
                SqlCommand cmdEliminar = new SqlCommand("DELETE FROM ProductoAdicional WHERE idCotizacion = @idCotizacion", con, transaccion);
                cmdEliminar.Parameters.AddWithValue("@idCotizacion", idCotizacion);
                cmdEliminar.ExecuteNonQuery();

                // Insertar los productos actualizados
                foreach (var producto in productos)
                {
                    SqlCommand cmdInsertar = new SqlCommand(
                        "INSERT INTO ProductoAdicional (idProducto, idCotizacion, cantidad) VALUES (@idProducto,@idCotizacion, @cantidad)", con, transaccion);

                    cmdInsertar.Parameters.AddWithValue("@idProducto", producto.idProducto);
                    cmdInsertar.Parameters.AddWithValue("@idCotizacion", idCotizacion);
                    cmdInsertar.Parameters.AddWithValue("@cantidad", producto.cantidad);

                    cmdInsertar.ExecuteNonQuery();
                }

                transaccion.Commit();
                return true;
            }
            catch
            {
                transaccion.Rollback();
                return false;
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }
        }



    }
}