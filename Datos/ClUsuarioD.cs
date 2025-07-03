using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace Seguridad_JSC.Datos
{
    public class ClUsuarioD
    {
        public bool MtdActualizarUsuario(ClUsuarioE Usuario)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection connection = conexion.MtdAbrirConexion();
            bool resultado = false;

            try
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = Usuario.IdUsuario;
                    command.Parameters.AddWithValue("@nombreUsuario", Usuario.nombreUsuario);
                    command.Parameters.AddWithValue("@documento", Usuario.documento);
                    command.Parameters.AddWithValue("@celular", Usuario.celular);
                    command.Parameters.AddWithValue("@email", Usuario.email);

                    string contraseñaCifrada;
                    if (!string.IsNullOrWhiteSpace(Usuario.contraseña))
                    {
                        contraseñaCifrada = CifrarContraseña(Usuario.contraseña);
                    }
                    else
                    {
                        contraseñaCifrada = ObtenerContraseñaActual(Usuario.IdUsuario, connection);
                    }

                    command.Parameters.AddWithValue("@contraseña", contraseñaCifrada);
                    command.Parameters.AddWithValue("@foto", Usuario.foto);

                    command.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                conexion.MtdcerrarConexion();
            }

            return resultado;
        }
        private string ObtenerContraseñaActual(int idUsuario, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT contraseña FROM Usuario WHERE idUsuario = @idUsuario", connection))
            {
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                object result = cmd.ExecuteScalar();
                return result?.ToString() ?? "";
            }
        }

        public DataTable MtdPerfil(int idUsuario)
        {
            ClConexion Conex = new ClConexion();
            SqlConnection conexion = Conex.MtdAbrirConexion();
            SqlCommand command = new SqlCommand("sp_UsuarioId", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@idUsuario", idUsuario);
            SqlDataAdapter table = new SqlDataAdapter(command);
            DataTable dtlListaDatos = new DataTable();
            table.Fill(dtlListaDatos);

            Conex.MtdcerrarConexion();

            return dtlListaDatos;
        }
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
                                    celular = filas["celular"].ToString(),
                                    estado = filas["estado"].ToString(),
                                    email = filas["email"].ToString(),
                                    foto = filas["foto"].ToString(),
                                    idRol = int.Parse(filas["idRol"].ToString()),
                                    nombreRol = filas["nombreRol"].ToString(),
                                    validacion = true
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
                Console.WriteLine("Error en la lista de técnicos: " + ex.Message);
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

        public bool MtdRegistroUsuario(ClUsuarioE oRegistro)
        {
            bool exito = false;

            try
            {
                if (VerificarDocumento(oRegistro.documento))
                {
                    throw new Exception("El documento ya está registrado.");
                }

                if (VerificarCorreo(oRegistro.email))
                {
                    throw new Exception("El correo ya está registrado.");
                }

                string contraseñaCifrada = CifrarContraseña(oRegistro.contraseña);

                ClConexion oConexion = new ClConexion();
                using (SqlConnection connection = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@documento", oRegistro.documento);
                        cmd.Parameters.AddWithValue("@nombreUsuario", oRegistro.nombreUsuario);
                        cmd.Parameters.AddWithValue("@celular", oRegistro.celular);
                        cmd.Parameters.AddWithValue("@email", oRegistro.email);
                        cmd.Parameters.AddWithValue("@password", contraseñaCifrada);
                        cmd.Parameters.AddWithValue("@foto", oRegistro.foto);
                        cmd.Parameters.AddWithValue("@idRol", oRegistro.idRol);

                        //SqlParameter usuarioP = new SqlParameter("@idUsuario", SqlDbType.Int);
                        //usuarioP.Direction = ParameterDirection.Output;
                        //cmd.Parameters.Add(usuarioP);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            exito = true;
                        }

                        //int idUsuario = (int)usuarioP.Value;
                        //HttpContext.Current.Session["idUsuario"] = idUsuario;
                    }
                    oConexion.MtdcerrarConexion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el usuario: " + ex.Message);
            }

            return exito;
        }

        private string CifrarContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public bool VerificarDocumento(int documento)
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection connection = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spVerificarDocumento", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@documento", documento);

                        int count = (int)cmd.ExecuteScalar();
                        oConexion.MtdcerrarConexion();

                        return count > 0;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el documento: " + ex.Message);
            }
        }

        public bool VerificarCorreo(string correo)
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection connection = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spVerificarCorreo", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", correo);

                        int count = (int)cmd.ExecuteScalar();
                        oConexion.MtdcerrarConexion();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el correo: " + ex.Message);
            }
        }
    }
}
