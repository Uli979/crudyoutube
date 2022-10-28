using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudyoutube
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            string servidor = "localhost";
            string bd = "youtube";
            string usuario = "root";
            string password = "123456";

            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User id= " + usuario + "; password =" + password + "";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);

                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;

            }


        }
    }
}
