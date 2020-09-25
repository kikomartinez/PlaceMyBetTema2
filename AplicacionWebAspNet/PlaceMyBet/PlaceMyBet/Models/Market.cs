using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Market
    {
        public Market(int marketID, int overOdds, int underOdds, int overMoney, int underMoney, int eventID)
        {
            MarketID = marketID;
            OverOdds = overOdds;
            UnderOdds = underOdds;
            OverMoney = overMoney;
            UnderMoney = underMoney;
            EventID = eventID;
        }

        public int MarketID { get; set; }
        public int OverOdds { get; set; }
        public int UnderOdds { get; set; }
        public int OverMoney { get; set; }
        public int UnderMoney { get; set; }
        public int EventID { get; set; }

    }
}