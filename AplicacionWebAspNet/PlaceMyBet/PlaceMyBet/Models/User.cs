using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class User
    {
        public User(string userID, string name, string surname, int age)
        {
            UserID = userID;
            Name = name;
            Surname = surname;
            Age = age;
        }

        public User()
        {

        }

        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public List<Bet> Bets { get; set; }
        public Account Account { get; set; }
    }
}