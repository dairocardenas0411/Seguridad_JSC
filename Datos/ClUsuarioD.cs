using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
	public class ClUsuarioD
	{
        public List<ClUsuarioE> MtdListaTecnicos()
        {
            List<ClUsuarioE> listaTecnicos = new List<ClUsuarioE>();
            ClConexion conexion = new ClConexion();
            try
            {
                SqlConnection sqlConnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListaTecnicos", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaTecnicos = new DataTable();
                        adapter.Fill(dtListaTecnicos);
                        if (dtListaTecnicos.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaTecnicos.Rows)
                            {
                                listaTecnicos.Add(new ClUsuarioE
                                {
                                    IdUsuario = int.Parse(filas["IdUsuario"].ToString()),
                                    documento = int.Parse(filas["documento"].ToString()),
                                    nombreUsuario = filas["nombreUsuario"].ToString(),
                                    cellular = filas["celular"].ToString(),
                                    estado = filas["estado"].ToString(),
                                    email = filas["email"].ToString(),
                                    contraseña = filas["contraseña"].ToString(),
                                    idRol = int.Parse(filas["idRol"].ToString()),
                                    nombreRol = filas["nombreRol"].ToString()
                                });
                                
                            }
                        }
                        else
                        {
                            listaTecnicos.Add(new ClUsuarioE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
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
            return listaTecnicos;
        }
    }
}