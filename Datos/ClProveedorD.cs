using Seguridad_JSC.Datos;
using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
    public class ClProveedorD
    {
        public bool MtdRegistroProveedor(ClProveedorE oRegistro)
        {
            bool exito = false;
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection connection = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistroProveedor", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre", oRegistro.nombre);
                        cmd.Parameters.AddWithValue("@documento", oRegistro.documento);
                        cmd.Parameters.AddWithValue("@celular", oRegistro.celular);
                        cmd.Parameters.AddWithValue("@empresa", oRegistro.empresa);
                        cmd.Parameters.AddWithValue("@email", oRegistro.email);
                        cmd.Parameters.AddWithValue("@imagen", oRegistro.imagen);

                        int filas = cmd.ExecuteNonQuery();
                        exito = filas > 0;
                    }
                    oConexion.MtdcerrarConexion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el Proveedor: " + ex.Message);
            }
            return exito;


        }


        public List<ClProveedorE> MtdListaProveedores()
        {
            List<ClProveedorE> listaProveedores = new List<ClProveedorE>();
            ClConexion conexion = new ClConexion();
            try
            {
                SqlConnection sqlConnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListarProveedores", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaProveedores = new DataTable();
                        adapter.Fill(dtListaProveedores);
                        if (dtListaProveedores.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaProveedores.Rows)
                            {
                                listaProveedores.Add(new ClProveedorE
                                {
                                    idProveedor = int.Parse(filas["idProveedor"].ToString()),
                                    nombre = filas["nombre"].ToString(),
                                    documento = int.Parse(filas["documento"].ToString()),
                                    celular = filas["celular"].ToString(),
                                    empresa = filas["empresa"].ToString(),
                                    email = filas["email"].ToString(),
                                    imagen = filas["imagen"].ToString(),
                                    TotalProductos = filas["TotalProductos"].ToString(),
                                    validacion = true
                                });
                            }
                        }
                        else
                        {
                            listaProveedores.Add(new ClProveedorE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la lista de proveedores: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.MtdcerrarConexion();
                }
            }
            return listaProveedores;
        }
        public DataTable MtdProductosProveedor(int idProveedor)
        {
            ClConexion Conex = new ClConexion();
            SqlConnection conexion = Conex.MtdAbrirConexion();
            SqlCommand command = new SqlCommand("sp_ProductosProveedor", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@idProveedor", idProveedor);
            SqlDataAdapter table = new SqlDataAdapter(command);
            DataTable dtlListaDatos = new DataTable();
            table.Fill(dtlListaDatos);

            Conex.MtdcerrarConexion();

            return dtlListaDatos;
        }

        public int ActualizarProveedor(ClProveedorE proveedor)
        {
            try
            {
                ClConexion Conex = new ClConexion();
                using (SqlConnection connection = Conex.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ActualizarProveedor", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);
                        cmd.Parameters.AddWithValue("@nombre", proveedor.nombre);
                        cmd.Parameters.AddWithValue("@documento", proveedor.documento);
                        cmd.Parameters.AddWithValue("@empresa", proveedor.empresa);
                        cmd.Parameters.AddWithValue("@email", proveedor.email);
                        cmd.Parameters.AddWithValue("@celular", proveedor.celular);
                        cmd.Parameters.AddWithValue("@imagen", proveedor.imagen ?? (object)DBNull.Value);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        Conex.MtdcerrarConexion();

                        return filasAfectadas;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el proveedor: " + ex.Message);


            }
        }
    }
}





