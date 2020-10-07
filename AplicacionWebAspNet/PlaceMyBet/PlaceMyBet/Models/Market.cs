using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Market
    {
        public Market(int marketID, int overOdds, int underOdds, int overMoney, int underMoney, float type, int eventID)
        {
            MarketID = marketID;
            OverOdds = overOdds;
            UnderOdds = underOdds;
            OverMoney = overMoney;
            UnderMoney = underMoney;
            Type = type;
            EventID = eventID;
        }

        public int MarketID { get; set; }
        public int OverOdds { get; set; }
        public int UnderOdds { get; set; }
        public int OverMoney { get; set; }
        public int UnderMoney { get; set; }
        public float Type { get; set; }
        public int EventID { get; set; }
    }

    public class MarketDTO
    {
        public MarketDTO(int overOdds, int underOdds, float type)
        {
            OverOdds = overOdds;
            UnderOdds = underOdds;
            Type = type;
        }

        public int OverOdds { get; set; }
        public int UnderOdds { get; set; }
        public float Type { get; set; }

    }
}