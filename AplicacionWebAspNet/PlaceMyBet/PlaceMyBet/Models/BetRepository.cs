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
            BetDTO bet = new BetDTO(result.GetString(1), result.GetFloat(2), result.GetFloat(3), result.GetString(4), result.GetString(5), marketTypes[marketTypeIndex]);
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

            //ELEMENTOS NECESARIOS PARA LA CONEXIÓN CON MYSQL
            MySqlConnection connection;
            MySqlCommand command;

            //CONSEGUIMOS INFO DEL MERCADO EN EL QUE SE REALIZA LA APUESTA
            Market marketFromThisBet;
            GetMarketInfo(bet, out connection, out command, out marketFromThisBet);

            //ASIGNAMOS EL VALOR A LA CUOTA QUE ENVIAREMOS SEGÚN EL TIPO DE APUESTA Y VALORES DEL MERCADO
            InsertBet(bet, connection, command, marketFromThisBet);

            float betMoney, overMoney, underMoney;
            betMoney = bet.BetMoney;
            overMoney = marketFromThisBet.OverMoney;
            underMoney = marketFromThisBet.UnderMoney;

            //UPDATE DE DINERO APOSTADO
            UpdateBetMoney(bet, connection, betMoney, overMoney, underMoney);

            //UPDATE DE CUOTA DE MERCADO:
            UpdateMarketOdds(bet, connection, marketFromThisBet, betMoney, overMoney, underMoney);

 
        }

        private void GetMarketInfo(Bet bet, out MySqlConnection connection, out MySqlCommand command, out Market market)
        {
            //CREAR CONEXIÓN CON MYSQL
            connection = Connect();
            command = connection.CreateCommand();

            //CREO UNA INSTANCIA DEL MERCADO
            market = null;

            //COMANDO PARA TOMAR LA INSTANCIA DE MERCADO NECESARIA
            MySqlCommand commandMarket = connection.CreateCommand();
            commandMarket.CommandText = "SELECT * FROM MERCADOS WHERE MERCADOS.id_mercado =" + bet.MarketID;

            try
            {
                connection.Open();

                result = commandMarket.ExecuteReader();



                while (result.Read())
                {
                    market = new Market(result.GetInt32(0), result.GetFloat(1), result.GetFloat(2), result.GetFloat(3), result.GetFloat(4), result.GetFloat(5), result.GetInt32(6));
                }

                connection.Close();

            }

            catch (MySqlException exception)
            {
                Debug.WriteLine("Se ha producido un error de conexión al crear instancia de mercado");
                connection.Close();
            }
        }

        private void InsertBet(Bet bet, MySqlConnection connection, MySqlCommand command, Market market)
        {
            float oddsToCommand;

            if (bet.TypeOfBet == "OVER")
            {
                oddsToCommand = market.OverOdds;
            }
            else
            {
                oddsToCommand = market.UnderOdds;
            }
  
            //COMANDO PARA INSERTAR LA APUESTA
            command.CommandText = "insert into APUESTAS(tipo_apuesta, dinero_apostado, cuota, fecha, ref_email_usuario, id_mercado) values ('" + bet.TypeOfBet + "','" + bet.BetMoney + "','" + oddsToCommand + "','" + bet.Date + "','" + bet.UserEmail + "','" + bet.MarketID + "');";

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
        }

        private void UpdateBetMoney(Bet bet, MySqlConnection connection, float betMoney, float overMoney, float underMoney)
        {
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

        private void UpdateMarketOdds(Bet bet, MySqlConnection connection, Market marketFromThisBet, float betMoney, float overMoney, float underMoney)
        {

            float probabilityOver = overMoney / (overMoney + underMoney);
            float probabilityUnder = underMoney / (overMoney + underMoney);
            float oddsOver = 1 / probabilityOver * 0.95f;
            float oddsUnder = 1 / probabilityUnder * 0.95f;

            MySqlCommand commandUpdateOverOdds = connection.CreateCommand();
            MySqlCommand commandUpdateUnderOdds = connection.CreateCommand();

            commandUpdateOverOdds.CommandText = "Update MERCADOS Set MERCADOS.cuota_over = " + oddsOver.ToString() + " WHERE MERCADOS.id_mercado = " + bet.MarketID + ";";
            commandUpdateUnderOdds.CommandText = "Update MERCADOS Set MERCADOS.cuota_under = " + oddsUnder.ToString() + " WHERE MERCADOS.id_mercado = " + bet.MarketID + ";";

            try
            {
                connection.Open();
                commandUpdateOverOdds.ExecuteNonQuery();
                commandUpdateUnderOdds.ExecuteNonQuery();
                connection.Close();
            }

            catch
            {
                Debug.Write("Fallo de conexión al actualizar cuotas");
                connection.Close();
            }
        }

    }
}
