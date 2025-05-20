using Seguridad_JSC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Seguridad_JSC.Datos
{
    public class ClLoginD
    {
        public ClLoginE MtdIngresoUsuarios(ClLoginE oUsuarios)
        {
            ClConexion oConexion = new ClConexion();
            SqlCommand command = new SqlCommand("spLogin", oConexion.MtdAbrirConexion());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = oUsuarios.email;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = oUsuarios.password;
            SqlDataReader reader = command.ExecuteReader();

            ClLoginE oUsuarioDatos = null;

            if (reader.Read())
            {
                oUsuarioDatos = new ClLoginE
                {
                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                    email = reader["email"].ToString(),
                    nombreUsuario = reader["nombreUsuario"].ToString(),
                    idRol = Convert.ToInt32(reader["idRol"]) // Ahora se obtiene idRol en vez de nombreRol
                };
            }

            reader.Close();
            oConexion.MtdcerrarConexion();

            return oUsuarioDatos;
        }
    }
}