using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Seguridad_JSC.Datos
{
	public class ClConexion
	{
        SqlConnection conexion = null;

        public ClConexion()
        {
            conexion = new SqlConnection("Data Source=.;Initial Catalog=dbSeguridad_JSC;Integrated Security=True;");
        }
        public SqlConnection MtdAbrirConexion()
        {
            conexion.Open();
            return conexion;
        }
        public void MtdcerrarConexion()
        {
            conexion.Close();
            
        }
       
    }
}

