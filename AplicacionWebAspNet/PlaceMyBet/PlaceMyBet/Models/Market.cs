using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Market
    {
        public Market(int marketID, float overOdds, float underOdds, float overMoney, float underMoney, float type, int eventID, List<Bet> bets)
        {
            MarketID = marketID;
            OverOdds = overOdds;
            UnderOdds = underOdds;
            OverMoney = overMoney;
            UnderMoney = underMoney;
            Type = type;
            EventID = eventID;
            Bets = bets;
        }

        public int MarketID { get; set; }
        public float OverOdds { get; set; }
        public float UnderOdds { get; set; }
        public float OverMoney { get; set; }
        public float UnderMoney { get; set; }
        public float Type { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        public List<Bet> Bets { get; set; }
    }

    public class MarketDTO
    {
        public MarketDTO(float overOdds, float underOdds, float type)
        {
            OverOdds = overOdds;
            UnderOdds = underOdds;
            Type = type;
        }

        public float OverOdds { get; set; }
        public float UnderOdds { get; set; }
        public float Type { get; set; }

    }
}