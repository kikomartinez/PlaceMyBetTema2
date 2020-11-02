using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class EventRepository : AbstractRepository<EventDTO>
    {
        public EventRepository()
        {
            tableName = "EVENTOS";
        }

        protected override EventDTO ConvertInfoToObject()
        {
           // EventDTO footballEvent = new EventDTO(result.GetString(1), result.GetString(2), result.GetString(3));
            return null;
        }
    }
}
