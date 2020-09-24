using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class BetsRepository : AbstractRepository<Bets>
    {
        public BetsRepository()
        {
            tableName = "apuesta";
        }

        protected override Bets ConvertInfoToObject()
        {
            Bets bet = new Bets(result.GetInt32(0), result.GetString(1), result.GetInt32(2), result.GetInt32(3), result.GetString(4), result.GetString(5), result.GetInt32(6));
            return bet;
        }
    }
}