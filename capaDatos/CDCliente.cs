using MySql.Data.MySqlClient;
using capaEntidad;
using System.Data;

namespace capaDatos
{
    public class CDCliente
    {
        string cadenaConexion = "Server=localhost;User=root;Password= ;Port=3306;database=crud_cs_mysql";

        public void PruebaConexion () 
        {
            // Creamos variable de tipo MySqlConnection
            MySqlConnection mySqlConnection = new MySqlConnection(cadenaConexion);

            try
            {
                mySqlConnection.Open();
            }
            catch (Exception except)
            {
                MessageBox.Show("Error al conectarse a la DB. " + except.Message);
                throw;
            }

            MessageBox.Show("DB conectada");
            
        }

        public void Crear(CECliente CECliente)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(cadenaConexion);
            mySqlConnection.Open();

            string query = $"INSERT INTO `clientes` (`nombre`, `apellido`, `foto`)" +
                $" VALUES ('{CECliente.Nombre}', '{CECliente.Apellido}', '{ MySql.Data.MySqlClient.MySqlHelper.EscapeString(CECliente.Foto) }');";
            
            MySqlCommand mySqlCommand = new(query, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            
        }

        public void Editar (CECliente CECliente)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(cadenaConexion);
            mySqlConnection.Open();

            string query = $"UPDATE `clientes`" +
                $" SET `nombre` = '{CECliente.Nombre}', `apellido` = '{CECliente.Apellido}', `foto` = '{MySql.Data.MySqlClient.MySqlHelper.EscapeString(CECliente.Foto)}'" +
                $" WHERE `id` = {CECliente.Id};";

            MySqlCommand mySqlCommand = new(query, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

        }

        public void Eliminar(CECliente CECliente)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(cadenaConexion);
            mySqlConnection.Open();

            string query = $"DELETE FROM `clientes` WHERE `id` = {CECliente.Id};";

            MySqlCommand mySqlCommand = new(query, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

        }

        public DataSet Listar()
        {
            MySqlConnection mySqlConnection = new MySqlConnection(cadenaConexion);
            mySqlConnection.Open();

            string query = $"SELECT * FROM `clientes` LIMIT 500";

            MySqlDataAdapter mySqlDataAdapter;
            DataSet dataSet = new DataSet();

            mySqlDataAdapter = new MySqlDataAdapter(query, mySqlConnection);
            mySqlDataAdapter.Fill(dataSet, "table_items");
            
            return dataSet;

        }
    }
}