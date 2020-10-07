using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public abstract class AbstractRepository<T> where T : class
    {
        protected string tableName;
        protected MySqlDataReader result;

        protected MySqlConnection Connect()
        {
            string connectionServer = "server=localhost;port=3306;database=placemybet;uid=root;password=maribel";
            MySqlConnection connection = new MySqlConnection(connectionServer);
            return connection;
        }

        internal virtual List<T> Retrieve()
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from " + tableName;

            try
            {
                connection.Open();
                result = command.ExecuteReader();

                T item = null;
                List<T> itemsToRetrieve = new List<T>();

                while (result.Read())
                {
                    item = ConvertInfoToObject();
                    itemsToRetrieve.Add(item);
                }

                connection.Close();
                return itemsToRetrieve;
            }

            catch (MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                connection.Close();
                return null;
            }
        }

        protected abstract T ConvertInfoToObject();

    }
}