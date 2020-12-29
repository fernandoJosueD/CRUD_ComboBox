using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ComboBox
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            String servidor = "localhost";
            String bd = "tiendas";
            String usuario = "root";
            String password = "Ninguna";

            String cadenaDeConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario +
                "; Password=" + password + "";

            //-------------------+
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaDeConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("error: " + ex.Message);
                return null;
            }




            
        }




    }//----------fin de class Conexion
}
