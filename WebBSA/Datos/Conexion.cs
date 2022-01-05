using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

using System.Data.SqlClient; 

namespace Datos
{
    internal class Conexion
    {
        private static readonly string _cnn = @"Data Source=DESKTOP-9RS7HI4\EQUIPO; Initial Catalog = bsadb; Integrated Security = true;";
        internal static string Cnn
        {
            get { return _cnn; }
        }
        internal static string ConexionUsuario(User usu)
        {
            if (usu != null)
                return @"Data Source=DESKTOP-9RS7HI4\EQUIPO;  Initial Catalog = bsadb; User ID=" + usu.Nick + "; Password='" + usu.Contraseña + "'";
            else
                return @"Data Source=DESKTOP-9RS7HI4\EQUIPO;  Initial Catalog = bsadb; Integrated Security = true;";
        }
    }
}
