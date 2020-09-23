using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class UsersRepository
    {

        private MySqlConnection Connect()
        {
            string connectionServer = "server=localhost;port=3306;database=placemybet;uid=root;password=maribel";
            MySqlConnection connection = new MySqlConnection(connectionServer);
            return connection;
        }

        internal List<Users> Retrieve()
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from usuario";

            try
            {
                connection.Open();
                MySqlDataReader result = command.ExecuteReader();

                Users user = null;
                List<Users> users = new List<Users>();

                while (result.Read())
                {
                    Debug.WriteLine("Recuparado:" + result.GetString(0) + " " + result.GetString(1) + " " + result.GetString(2) + " " + result.GetInt32(3));
                    user = new Users(result.GetString(0), result.GetString(1), result.GetString(2), result.GetInt32(3));
                    users.Add(user);
                }

                connection.Close();
                return users;
            }
            
            catch(MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                return null;
            }
        }
    }
}