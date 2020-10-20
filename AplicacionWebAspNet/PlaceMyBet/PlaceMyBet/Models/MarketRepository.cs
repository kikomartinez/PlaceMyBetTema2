using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class MarketRepository : AbstractRepository<MarketDTO>
    {

        public MarketRepository()
        {
            tableName = "MERCADOS";
        }

        protected override MarketDTO ConvertInfoToObject()
        {
            MarketDTO market = new MarketDTO(result.GetInt32(1), result.GetInt32(2), result.GetFloat(5));
            return market;
        }

        internal MarketDTO RetrieveByEventAndMarketType(int eventID, float marketType)
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * from MERCADOS WHERE MERCADOS.id_partido = @A1 AND MERCADOS.tipo = @A2";
            command.Parameters.AddWithValue("@A1", eventID);
            command.Parameters.AddWithValue("@A2", marketType);

            try
            {
                connection.Open();
                MySqlDataReader result = command.ExecuteReader();

                MarketDTO market = null;
                while (result.Read())
                {
                    Debug.WriteLine("RECUPERADO MERCADO: " + result.GetInt32(1), result.GetInt32(2), result.GetFloat(5));
                    market = new MarketDTO(result.GetInt32(1), result.GetInt32(2), result.GetFloat(5));
                }

                connection.Close();
                return market;
            }

            catch(MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión al intentar conseguir mercado");
                connection.Close();
                return null;
            }
        }
    }
}