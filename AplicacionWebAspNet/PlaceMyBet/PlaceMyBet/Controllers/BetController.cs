using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class BetController : AbstractController<BetRepository, BetDTOExpanded>
    {
        public void Post([FromBody]Bet bet)
        {
            var repository = new BetRepository();
            repository.Save(bet);
        }

        public List<BetDTOLessInfo> GetBetByEmailAndMarketType(string email, float marketType)
        {
            var repository = new BetRepository();
            List<BetDTOLessInfo> bets = repository.RetrieveByEmailAndMarketType(email, marketType);
            return bets;
        }
    }
}
