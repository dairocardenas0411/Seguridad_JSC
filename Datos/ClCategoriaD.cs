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
    public class ClCategoriaD
    {
        public bool MtdRegistroCategoria(ClCategoriaE oCategoria)
        {
            ClConexion oConexion = new ClConexion();
            SqlConnection connection = oConexion.MtdAbrirConexion();
            bool exito = false;
            try
            {
                using (SqlCommand comand = new SqlCommand("sp_RegistrarCategoria", connection))
                {
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.Parameters.AddWithValue("@nombreCategoria", oCategoria.nombreCategoria);
                    comand.Parameters.AddWithValue("@descripcion", oCategoria.descripcion);
                    comand.ExecuteNonQuery();
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la categoria: " + ex.Message);
            }
            finally
            {
                oConexion.MtdcerrarConexion();
            }
            return exito;
        }

        public List<ClCategoriaE> MtdListaCategoria()
        {
            List<ClCategoriaE> listaCategoria = new List<ClCategoriaE>();
            ClConexion conexion = new ClConexion();
            try
            {
                SqlConnection sqlConnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListaCategoria", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaCategoria = new DataTable();
                        adapter.Fill(dtListaCategoria);
                        if (dtListaCategoria.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaCategoria.Rows)
                            {
                                listaCategoria.Add(new ClCategoriaE
                                {
                                    idCategoria = int.Parse(filas["idCategoria"].ToString()),
                                    nombreCategoria = filas["nombreCategoria"].ToString(),
                                    descripcion = filas["descripcion"].ToString()
                                });
                            }
                        }
                        else
                        {
                            listaCategoria.Add(new ClCategoriaE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la lista de categorías: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.MtdcerrarConexion();
                }
            }
            return listaCategoria;
        }

        public bool MtdActualizarCategoria(ClCategoriaE oCategoria)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection conn = conexion.MtdAbrirConexion();
            bool exito = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("spActualizarCategoria", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("@nombreCategoria", oCategoria.nombreCategoria);
                    cmd.Parameters.AddWithValue("@descripcion", oCategoria.descripcion);
                    cmd.ExecuteNonQuery();
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la categoría: " + ex.Message);
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }

            return exito;
        }

        public bool MtdEliminarCategoria(int idCategoria)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection conn = conexion.MtdAbrirConexion();
            bool exito = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("spEliminarCategoria", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                    cmd.ExecuteNonQuery();
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría: " + ex.Message);
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }

            return exito;
        }

    }
}

               