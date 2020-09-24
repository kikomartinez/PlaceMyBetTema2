using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class MarketsRepository : AbstractRepository<Markets>
    {

        public MarketsRepository()
        {
            tableName = "mercado";
        }

        protected override Markets ConvertInfoToObject()
        {
            Markets market = new Markets(result.GetInt32(0), result.GetInt32(1), result.GetInt32(2), result.GetInt32(3), result.GetInt32(4), result.GetInt32(5));
            return market;
        }
    }
}