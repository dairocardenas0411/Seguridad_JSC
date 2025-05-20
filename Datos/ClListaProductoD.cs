using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
    public class ClListaProductoD
    {
        public List<ClProductoE> MtdListarProducto()
        {
            List<ClProductoE> listaProductos = new List<ClProductoE>();
            ClConexion conexion = new ClConexion();
            try
            {
                SqlConnection sqlConnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Productos", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaProductos = new DataTable();
                        adapter.Fill(dtListaProductos);
                        if (dtListaProductos.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaProductos.Rows)
                            {
                                listaProductos.Add(new ClProductoE
                                {
                                    idProducto = int.Parse(filas["idProducto"].ToString()),
                                    nombreProducto = filas["nombreProducto"].ToString(),
                                    descripcion = filas["descripcion"].ToString(),
                                    codigo = filas["codigo"].ToString(),
                                    precioUnitario = decimal.Parse(filas["precioUnitario"].ToString()),
                                    cantidadstock = int.Parse(filas["cantidadstock"].ToString()),
                                    estado = filas["estado"].ToString(),
                                    imagen = filas["imagen"].ToString(),
                                    idCategoria = int.Parse(filas["idCategoria"].ToString()),
                                    idProveedor = int.Parse(filas["idProveedor"].ToString()),
                                    idUsuario = int.Parse(filas["idUsuario"].ToString()),
                                    validacion = true
                                });
                            }
                        }
                        else
                        {
                            listaProductos.Add(new ClProductoE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Error en la lista de productos: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.MtdcerrarConexion();
                }
            }
            return listaProductos;
        }
    }
}