using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class MarketRepository : AbstractRepository<Market>
    {

        public MarketRepository()
        {
            tableName = "MERCADOS";
        }

        protected override Market ConvertInfoToObject()
        {
            Market market = new Market(result.GetInt32(0), result.GetInt32(1), result.GetInt32(2), result.GetInt32(3), result.GetInt32(4), result.GetInt32(5));
            return market;
        }
    }
}