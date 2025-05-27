using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using Seguridad_JSC.Vista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
    public class ClCotizacionD
    {

        public int MtdRegistroCotizacion(ClCotizacionE oCotizacion)
        {
            if (oCotizacion == null)
                throw new ArgumentNullException(nameof(oCotizacion), "La cotización no puede ser nula.");

            ClConexion conexion = new ClConexion();
            SqlConnection connection = conexion.MtdAbrirConexion();
            int idGenerado = 0;

            try
            {
                using (SqlCommand command = new SqlCommand("Sp_RegistrarCotizacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros del cliente
                    command.Parameters.AddWithValue("@nombreCliente", oCotizacion.nombreCliente);
                    command.Parameters.AddWithValue("@apellidoCliente", oCotizacion.apellidoCliente);
                    command.Parameters.AddWithValue("@documento", oCotizacion.documento);
                    command.Parameters.AddWithValue("@telefono", oCotizacion.telefono);
                    command.Parameters.AddWithValue("@email", oCotizacion.email);
                    command.Parameters.AddWithValue("@direccionInstalacion", oCotizacion.direccionInstalacion);

                    // Parámetros de la cotización
                    command.Parameters.AddWithValue("@observaciones", oCotizacion.observaciones);
                    command.Parameters.AddWithValue("@tipoTrabajo", oCotizacion.tipoTrabajo);
                    command.Parameters.AddWithValue("@observacionesTrabajo", oCotizacion.observacionesTrabajo ?? "");
                    command.Parameters.AddWithValue("@idUsuario", oCotizacion.idUsuario == 0 ? (object)DBNull.Value : oCotizacion.idUsuario);

                    // Crear DataTable de productos con 3 columnas
                    DataTable dtProductos = new DataTable();
                    dtProductos.Columns.Add("idProducto", typeof(int));
                    dtProductos.Columns.Add("cantidad", typeof(int));
                    dtProductos.Columns.Add("valorProductoA", typeof(decimal)); // Debe coincidir con el tipo SQL

                    // Llenar DataTable con datos
                    if (oCotizacion.Productos != null && oCotizacion.Productos.Count > 0)
                    {
                        foreach (var producto in oCotizacion.Productos)
                        {
                            decimal valorProductoA = producto.valorProductoA; // Asegúrate que tu objeto tiene esta propiedad decimal
                            dtProductos.Rows.Add(producto.idProducto, producto.cantidad, valorProductoA);
                        }
                    }

                    // Parámetro tipo tabla
                    SqlParameter productosParam = command.Parameters.AddWithValue("@Productos", dtProductos);
                    productosParam.SqlDbType = SqlDbType.Structured;
                    productosParam.TypeName = "dbo.TipoProductoCotizacion";

                    // Ejecutar y obtener el ID generado
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        idGenerado = id;
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el ID generado de la cotización.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la cotización: " + ex.Message);
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }

            return idGenerado;
        }



        public List<ClCotizacionE> MtdListadCotizacionPendiente()
        {
            List<ClCotizacionE> listaCotizacionPendiente = new List<ClCotizacionE>();
            ClConexion conexion = new ClConexion();

            try
            {
                SqlConnection sqlconnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListaTrabajo", sqlconnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaCotizacionPendiente = new DataTable();
                        adapter.Fill(dtListaCotizacionPendiente);
                        if (dtListaCotizacionPendiente.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaCotizacionPendiente.Rows)
                            {
                                listaCotizacionPendiente.Add(new ClCotizacionE
                                {
                                    idCotizacion = int.Parse(filas["idCotizacion"].ToString()),
                                    nombreCliente = filas["nombreCliente"].ToString(),
                                    apellidoCliente = filas["apellidoCliente"].ToString(),
                                    documento = int.Parse(filas["documento"].ToString()),
                                    telefono = filas["telefono"].ToString(),
                                    email = filas["email"].ToString(),
                                    direccionInstalacion = filas["direccionInstalacion"].ToString(),
                                    estado = filas["estado"].ToString(),
                                    fechaCotizacion = Convert.ToDateTime(filas["fechaCotizacion"]),
                                    observaciones = filas["observaciones"].ToString(),
                                    validacion = true
                                });
                            }
                        }
                        else
                        {
                            listaCotizacionPendiente.Add(new ClCotizacionE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la lista de cotizaciones: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.MtdcerrarConexion();
                }

            }
            return listaCotizacionPendiente;
        }

        public DataTable MtdInfoProducto(int idCotizacion)
        {
            ClConexion Conex = new ClConexion();
            SqlConnection conexion = Conex.MtdAbrirConexion();
            SqlCommand command = new SqlCommand("sp_ProductosAdicionales", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@idCotizacion", idCotizacion);
            SqlDataAdapter table = new SqlDataAdapter(command);
            DataTable dtlListaDatos = new DataTable();
            table.Fill(dtlListaDatos);
        
            return dtlListaDatos;
        }
        public DataTable MtdInfoCotizacion(int idCotizacion)
        {
            ClConexion conex = new ClConexion();
            SqlConnection conexion = conex.MtdAbrirConexion();
            SqlCommand command = new SqlCommand("sp_CotizacionId", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@idCotizacion", idCotizacion);
            SqlDataAdapter table = new SqlDataAdapter(command);
            DataTable dtlListaDatos = new DataTable();
            table.Fill(dtlListaDatos);
            foreach (DataRow row in dtlListaDatos.Rows)
            {
                if (row["fechaCotizacion"] != DBNull.Value)
                {
                    row["fechaCotizacion"] = Convert.ToDateTime(row["fechaCotizacion"]).ToString("dd-MM-yyyy");
                }

            }
            return dtlListaDatos;
        }


        public bool MtdActualizarTrabajo(ClCotizacionE trabajo)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection connection = conexion.MtdAbrirConexion();
            bool resultado = false;
            try
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarTrabajo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@idCotizacion", SqlDbType.Int).Value = trabajo.idCotizacion;
                    command.Parameters.AddWithValue("@nombreCliente", trabajo.nombreCliente);
                    command.Parameters.AddWithValue("@apellidoCliente", trabajo.apellidoCliente);
                    command.Parameters.AddWithValue("@documento", trabajo.documento);
                    command.Parameters.AddWithValue("@telefono", trabajo.telefono);
                    command.Parameters.AddWithValue("@email", trabajo.email);
                    command.Parameters.AddWithValue("@direccionInstalacion", trabajo.direccionInstalacion);
                    command.Parameters.AddWithValue("@estado", trabajo.estado);
                    command.Parameters.AddWithValue("@cantidad", trabajo.cantidad);
                    command.Parameters.AddWithValue("@observaciones", trabajo.observaciones);
                    command.Parameters.AddWithValue("@valorProducto", trabajo.valorProducto); 
                    command.Parameters.AddWithValue("@valorInstalacion", trabajo.valorInstalacion);
                    command.Parameters.AddWithValue("@cargosAdicionales", trabajo.cargosAdicionales);
                    command.Parameters.AddWithValue("@valorTotal", trabajo.valorTotal); 
                    command.Parameters.AddWithValue("@tipoTrabajo", trabajo.tipoTrabajo);
                    command.Parameters.AddWithValue("@observacionesTrabajo", trabajo.observacionesTrabajo);
                    command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = trabajo.idUsuarioT;
                    command.Parameters.Add("@idProducto", SqlDbType.Int).Value = trabajo.idProducto;

                    command.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la cotización: " + ex.Message);
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }
            return resultado;
        }



        public DataTable MtdListaTrabajoTecnico(int idTecnico)
        {
            ClConexion conex = new ClConexion();
            SqlConnection conexion = conex.MtdAbrirConexion();
            SqlCommand command = new SqlCommand("sp_ListarTrabajoTecnico", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@idUsuarioT", idTecnico);
            SqlDataAdapter table = new SqlDataAdapter(command);
            DataTable dtlListaDatos = new DataTable();
            table.Fill(dtlListaDatos);
            foreach (DataRow row in dtlListaDatos.Rows)
            {
                if (row["fechaCotizacion"] != DBNull.Value)
                {
                    row["fechaCotizacion"] = Convert.ToDateTime(row["fechaCotizacion"]).ToString("dd-MM-yyyy");
                }
            }
            return dtlListaDatos;
        }

       

    }
}