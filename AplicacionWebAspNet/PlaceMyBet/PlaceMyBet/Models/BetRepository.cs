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
                Debug.WriteLine("Se ha producido un error de conexión 1");
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
                Debug.WriteLine("Se ha producido un error de conexión 2");
                connection.Close();
                return null;
            }
        }

        protected override BetDTO ConvertInfoToObject()
        {
            BetDTO bet = new BetDTO(result.GetString(1), result.GetInt32(2), result.GetInt32(3), result.GetString(4), result.GetString(5), marketTypes[marketTypeIndex]);
            marketTypeIndex++;
            return bet;
        }

        internal void Save(Bet bet)
        {
            //ADAPTAR CULTURA PARA EVITAR PROBLEMAS CON LA COMA
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");

            culInfo.NumberFormat.NumberDecimalSeparator = ".";

            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            //CREAR CONEXIÓN CON MYSQL
            MySqlConnection connection = Connect();
            MySqlCommand command = connection.CreateCommand();

            //CREO UNA INSTANCIA DEL MERCADO
            Market market = null;

            //COMANDO PARA TOMAR LA INSTANCIA DE MERCADO NECESARIA
            MySqlCommand commandMarket = connection.CreateCommand();
            commandMarket.CommandText = "SELECT * FROM MERCADOS WHERE MERCADOS.id_mercado =" + bet.MarketID ;

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
                Debug.WriteLine("Se ha producido un error de conexión al crear instancia de mercado");
                connection.Close(); 
            }

            //ASIGNAMOS EL VALOR A LA CUOTA QUE ENVIAREMOS SEGÚN EL TIPO DE APUESTA Y VALORES DEL MERCADO
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

            //COMANDO PARA INSERTAR LA APUESTA
            command.CommandText = "insert into APUESTAS(tipo_apuesta, dinero_apostado, cuota, fecha, ref_email_usuario, id_mercado) values ('" + bet.TypeOfBet + "','" + bet.BetMoney + "','" + oddsToCommand + "','" + bet.Date + "','" + bet.UserEmail + "','" + bet.MarketID + "');";
            Debug.WriteLine("EL COMANDO INSERTAR APUESTA ES: " + command.CommandText);
            
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión al insertar apuesta");
                connection.Close();
            }


            //UPDATE DE CUOTA DE MERCADO:
            float betMoney = bet.BetMoney;
            float overMoney = market.OverMoney;
            float underMoney = market.UnderMoney;

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

            commandUpdateOdds.CommandText = "Update MERCADOS Set MERCADOS." + typeOfOddFromBet + " = " + odds.ToString() + " WHERE MERCADOS.id_mercado = " + bet.MarketID + ";";
            Debug.WriteLine("comando UPDATE ODDS ES: " + commandUpdateOdds.CommandText);

            try
            {
                connection.Open();
                commandUpdateOdds.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión al actualizar cuota");
                connection.Close();
            }

            //UPDATE DE DINERO APOSTADO

            string typeOfMoney;
            float money;
            if (bet.TypeOfBet == "OVER")
            {
                typeOfMoney = "dinero_over";
                money = betMoney + overMoney;
            }
            else
            {
                typeOfMoney = "dinero_under";
                money = betMoney + underMoney;
            }

            MySqlCommand commandUpdateMoney = connection.CreateCommand();
            commandUpdateMoney.CommandText = "Update MERCADOS Set MERCADOS." + typeOfMoney + " = " + money.ToString() + " WHERE MERCADOS.id_mercado = " + bet.MarketID + ";";
            Debug.WriteLine("comando UPDATE MONEY ES: " + commandUpdateMoney.CommandText);


            try
            {
                connection.Open();
                commandUpdateMoney.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión al actualizar dinero mercado");
                connection.Close();
            }

        }

    }
}

//"Update MERCADOS Set MERCADOS.cuota_under = 10 Where MERCADOS.id_mercado = 3;"
//UPDATE MERCADOS SET MERCADOS.dinero_over = 500 WHERE MERCADOS.id_mercado = 3;