using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Bet
    {
        public Bet(int betID, string typeOfBet, float betMoney, float odd, string date, string userID, int marketID)
        {
            BetID = betID;
            TypeOfBet = typeOfBet;
            BetMoney = betMoney;
            Odd = odd;
            Date = date;
            UserID = userID;
            MarketID = marketID;
        }

        public int BetID { get; set; }
        public string TypeOfBet { get; set; }
        public float BetMoney { get; set; }
        public float Odd { get; set; }
        public string Date { get; set; }
        public string UserID { get; set; }
        public int MarketID { get; set; }
        public Market Market { get; set; }
        public User User { get; set; }
    }

    public abstract class BetDTO
    {

    }

    public class BetDTOExpanded : BetDTO
    {
        public BetDTOExpanded(string typeOfBet, float betMoney, float odd, string date, string userEmail, float typeOfMarket)
        {
            TypeOfBet = typeOfBet;
            BetMoney = betMoney;
            Odd = odd;
            Date = date;
            UserEmail = userEmail;
            TypeOfMarket = typeOfMarket;
        }

        public string TypeOfBet { get; set; }
        public float BetMoney { get; set; }
        public float Odd { get; set; }
        public string Date { get; set; }
        public string UserEmail { get; set; }
        public float TypeOfMarket { get; set; }
    }

    public class BetDTOLessInfoA : BetDTO
    {
        public BetDTOLessInfoA(int eventID, string typeOfBet, float odd, float betMoney)
        {
            EventID = eventID;
            TypeOfBet = typeOfBet;
            Odd = odd;
            BetMoney = betMoney;

        }

        public int EventID { get; set; }
        public string TypeOfBet { get; set; }
        public float Odd { get; set; }
        public float BetMoney { get; set; }
    }

    public class BetDTOLessInfoB : BetDTO
    {
        public BetDTOLessInfoB(float marketType, string typeOfBet, float odd, float betMoney)
        {
            MarketType = marketType;
            TypeOfBet = typeOfBet;
            Odd = odd;
            BetMoney = betMoney;
        }

        public float MarketType { get; set; }
        public string TypeOfBet { get; set; }
        public float Odd { get; set; }
        public float BetMoney { get; set; }
    }
}