using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class EventsRepository : AbstractRepository<Events>
    {
        public EventsRepository()
        {
            tableName = "EVENTOS";
        }

        protected override Events ConvertInfoToObject()
        { 
            Events footballEvent = new Events(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3));
            return footballEvent;
        }
    }
}
