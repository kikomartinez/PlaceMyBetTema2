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

        internal Users Retrieve()
        {
            //Users user = new Users("prueba@gmail.com", "Pepe", "Martínez", 25);

            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from usuario";

            connection.Open();
            MySqlDataReader result = command.ExecuteReader();

            Users user = null;

            if (result.Read())
            {
                Debug.WriteLine("Recuparado:" + result.GetString(0) + " " + result.GetString(1) + " " + result.GetString(2) + " " + result.GetInt32(3));
                user = new Users(result.GetString(0), result.GetString(1), result.GetString(2), result.GetInt32(3));
            }

            connection.Close();
            return user;
        }
    }
}