using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class User
    {
        public User(string email, string name, string surname, int age, List<Bet> bets)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Age = age;
            Bets = bets;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public List<Bet> Bets { get; set; }
        public Account Account { get; set; }
    }
}