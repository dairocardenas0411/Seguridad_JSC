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
        public bool MtdRegistroCotizacion(ClCotizacionE oCotizacion)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection connection = conexion.MtdAbrirConexion();
            bool resultado = false;

            try
            {
                using (SqlCommand command = new SqlCommand("Sp_RegistrarCotizacion", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombreCliente", oCotizacion.nombreCliente);
                    command.Parameters.AddWithValue("@apellidoCliente", oCotizacion.apellidoCliente);
                    command.Parameters.AddWithValue("@documento", oCotizacion.documento);
                    command.Parameters.AddWithValue("@telefono", oCotizacion.telefono);
                    command.Parameters.AddWithValue("@email", oCotizacion.email);
                    command.Parameters.AddWithValue("@direccionInstalacion", oCotizacion.direccionInstalacion);
                    command.Parameters.AddWithValue("@idProducto", oCotizacion.idProducto);
                    command.Parameters.AddWithValue("@cantidad", oCotizacion.cantidad);
                    command.Parameters.AddWithValue("@observaciones", oCotizacion.observaciones);

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
        public List<ClCotizacionE> MtdListadCotizacionPendiente()
        {
            List<ClCotizacionE> listaCotizacionPendiente = new List<ClCotizacionE>();
            ClConexion conexion = new ClConexion();

            try
            {
                SqlConnection sqlconnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListaCotizacion", sqlconnection))
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
                                    nombreProducto = filas["nombreProducto"].ToString(),
                                    idProducto = int.Parse(filas["idProducto"].ToString()),
                                    cantidad = int.Parse(filas["cantidad"].ToString()),
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

    }

}