using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}