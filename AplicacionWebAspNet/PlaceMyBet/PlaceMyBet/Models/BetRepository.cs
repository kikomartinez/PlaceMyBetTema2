using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class BetRepository : AbstractRepository<BetDTO>
    {
        List<float> marketTypes = new List<float>();
        int marketTypeIndex = 0;

        public BetRepository()
        {
            tableName = "APUESTAS";
        }

        internal override List<BetDTO> Retrieve()
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from " + tableName;
            MySqlCommand commandMarket = connection.CreateCommand();
            commandMarket.CommandText = "SELECT MERCADOS.tipo FROM MERCADOS, APUESTAS WHERE(APUESTAS.id_mercado = MERCADOS.id_mercado)";

            try
            {
                connection.Open();

                result = commandMarket.ExecuteReader();
                
    
                while (result.Read())
                {
                   marketTypes.Add(result.GetFloat(0));
                }

                connection.Close();

            }

            catch (MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                connection.Close();
                return null;
            }

            try
            {
                connection.Open();

                result = command.ExecuteReader();
                

                BetDTO item = null;
                List<BetDTO> itemsToRetrieve = new List<BetDTO>();

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


        protected override BetDTO ConvertInfoToObject()
        {
            Debug.WriteLine(marketTypes[marketTypeIndex]);
            BetDTO bet = new BetDTO(result.GetString(1), result.GetInt32(2), result.GetInt32(3), result.GetString(4), result.GetString(5), marketTypes[marketTypeIndex]);
            marketTypeIndex++;
            return bet;
        }
    }
}

