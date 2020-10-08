using MySql.Data.MySqlClient;
using PlaceMyBet.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        internal void Save(Bet bet)
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");

            culInfo.NumberFormat.NumberDecimalSeparator = ".";

            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;


            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();
            int marketOdds = 0;
            Market market = null;

            MySqlCommand commandMarket = connection.CreateCommand();
            commandMarket.CommandText = "SELECT * FROM MERCADOS WHERE MERCADOS.id_mercado =" + bet.MarketID ;
            Debug.WriteLine("COMANDO!!!!! " + commandMarket.CommandText);

            /*if (bet.TypeOfBet == "OVER")
            {
                commandMarket.CommandText = "SELECT MERCADOS.cuota_over FROM MERCADOS, APUESTAS WHERE(MERCADOS.id_mercado = " + bet.MarketID + " )";
            }
            else
            {
                commandMarket.CommandText = "SELECT MERCADOS.cuota_under FROM MERCADOS, APUESTAS WHERE(MERCADOS.id_mercado = " + bet.MarketID + " )";
            }
            Debug.WriteLine("COMANDO!!!!! " + commandMarket.CommandText);
            */
            try
            {
                connection.Open();

                result = commandMarket.ExecuteReader();
                


                while (result.Read())
                {
                    market = new Market(result.GetInt32(0), result.GetInt32(1), result.GetInt32(2), result.GetInt32(3), result.GetInt32(4), result.GetFloat(5), result.GetInt32(6));
                }

                connection.Close();

            }

            catch (MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
                connection.Close(); 
            }

            float oddsToCommand;

            if (bet.TypeOfBet == "OVER")
            {
                oddsToCommand = market.OverOdds;
            }
            else
            {
                oddsToCommand = market.UnderOdds;
            }

            Debug.Write("MARKET ODDS VALE:" + oddsToCommand);
            command.CommandText = "insert into APUESTAS(tipo_apuesta, dinero_apostado, cuota, fecha, ref_email_usuario, id_mercado) values ('" + bet.TypeOfBet + "','" + bet.BetMoney + "','" + oddsToCommand + "','" + bet.Date + "','" + bet.UserEmail + "','" + bet.MarketID + "');";
            Debug.WriteLine("COMMANDO ES ES ES ES " + command.CommandText);
            

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión");
                connection.Close();
            }

            //UPDATE DE CUOTA DE MERCADO:

            int betMoney = bet.BetMoney;
            int overMoney = market.OverMoney;
            int underMoney = market.UnderMoney;

            float probability = betMoney / (overMoney + underMoney);
            float odds = 1 / (probability) * 0.95f;
            Debug.WriteLine("LAS ODDS SON " + odds);



            MySqlCommand commandUpdateOdds = connection.CreateCommand();
            string typeOfOddFromBet;

            if (bet.TypeOfBet == "OVER")
            {
                typeOfOddFromBet = "cuota_over";
            }
            else
            {
                typeOfOddFromBet = "cuota_under";
            }

            commandUpdateOdds.CommandText = "Update MERCADOS Set MERCADOS." + typeOfOddFromBet + " = " + "0." + float.Parse(odds.ToString(), CultureInfo.InvariantCulture) + " WHERE MERCADOS.id_mercado = " + bet.MarketID + ";";
            Debug.WriteLine("comando " + commandUpdateOdds.CommandText);

            try
            {
                connection.Open();
                commandUpdateOdds.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión");
                connection.Close();
            }
        }

    }
}

//"Update MERCADOS Set MERCADOS.cuota_under = 10 Where MERCADOS.id_mercado = 3;"