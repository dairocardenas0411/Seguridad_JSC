using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
	public class ClRolD
	{
        public List<ClRolE> MtdListaRol()
        {
            List<ClRolE> listaRol = new List<ClRolE>();
            ClConexion conexion = new ClConexion();
            try
            {
                SqlConnection sqlConnection = conexion.MtdAbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ListaRol", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtListaRol = new DataTable();
                        adapter.Fill(dtListaRol);
                        if (dtListaRol.Rows.Count > 0)
                        {
                            foreach (DataRow filas in dtListaRol.Rows)
                            {
                                listaRol.Add(new ClRolE
                                {
                                 
                                    idRol = int.Parse(filas["idRol"].ToString()),
                                    nombreRol = filas["nombreRol"].ToString(),
                                   
                                });
                            }
                        }
                        else
                        {
                            listaRol.Add(new ClRolE
                            {
                                validacion = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la lista de técnicos: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.MtdcerrarConexion();
                }
            }
            return listaRol;
        }
    }
}