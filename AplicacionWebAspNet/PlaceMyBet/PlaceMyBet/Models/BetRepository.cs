using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class BetRepository : AbstractRepository<Bet>
    {
        public BetRepository()
        {
            tableName = "APUESTAS";
        }

        protected override Bet ConvertInfoToObject()
        {
            Bet bet = new Bet(result.GetInt32(0), result.GetString(1), result.GetInt32(2), result.GetInt32(3), result.GetString(4), result.GetString(5), result.GetInt32(6));
            return bet;
        }
    }
}