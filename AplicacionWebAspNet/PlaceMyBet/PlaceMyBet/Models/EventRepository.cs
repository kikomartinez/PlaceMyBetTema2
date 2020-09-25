using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class EventRepository : AbstractRepository<Event>
    {
        public EventRepository()
        {
            tableName = "EVENTOS";
        }

        protected override Event ConvertInfoToObject()
        { 
            Event footballEvent = new Event(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3));
            return footballEvent;
        }
    }
}
