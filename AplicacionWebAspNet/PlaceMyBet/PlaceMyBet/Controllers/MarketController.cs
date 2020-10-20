using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class MarketController : AbstractController<MarketRepository, MarketDTO>
    {
        //localhost:44346/api/Market?eventID={eventIDvalue}&marketType={marketTypeValue}
        public MarketDTO GetMarketFromEventAndMarketType(int eventID, float marketType)
        {
            var repository = new MarketRepository();
            MarketDTO market = repository.RetrieveByEventAndMarketType(eventID, marketType);
            return market;
        }
    }
}
