using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Events
    {
        public Events(int eventID, string localTeam, string visitorTeam, string date)
        {
            EventID = eventID;
            LocalTeam = localTeam;
            VisitorTeam = visitorTeam;
            Date = date;
        }

        public int EventID { get; set; }
        public string LocalTeam { get; set; }
        public string VisitorTeam { get; set; }
        public string Date { get; set; }

    }
}