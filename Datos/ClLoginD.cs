using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Seguridad_JSC.Datos
{
    public class ClLoginD
    {
        private string CifrarContraseña(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public ClLoginE MtdIngresoUsuarios(ClLoginE oUsuarios)
        {
            ClConexion oConexion = new ClConexion();
            SqlCommand command = new SqlCommand("spLogin", oConexion.MtdAbrirConexion());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = oUsuarios.email;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = CifrarContraseña(oUsuarios.password);
            SqlDataReader reader = command.ExecuteReader();

            ClLoginE oUsuarioDatos = null;

            if (reader.Read())
            {
                oUsuarioDatos = new ClLoginE
                {
                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                    email = reader["email"].ToString(),
                    nombreUsuario = reader["nombreUsuario"].ToString(),
                    foto = reader["foto"].ToString(),
                    idRol = Convert.ToInt32(reader["idRol"]), 
                    nombreRol=reader["nombreRol"].ToString()
                };
            }

            reader.Close();
            oConexion.MtdcerrarConexion();

            return oUsuarioDatos;
        }
    }
}