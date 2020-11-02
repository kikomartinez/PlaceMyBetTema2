using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Account
    {

        public Account(int accountID, float currentMoney, string bank, string userID)
        {
            AccountID = accountID;
            CurrentMoney = currentMoney;
            Bank = bank;
            UserID = userID;
        }


        public int AccountID { get; set; }
        public float CurrentMoney { get; set; }
        public string Bank { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}