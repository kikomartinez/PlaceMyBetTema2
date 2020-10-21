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
        [Authorize]
        public void Post([FromBody]Bet bet)
        {
            var repository = new BetRepository();
            repository.Save(bet);
        }

        //localhost:44346/api/Bet?&email={emailValue}&marketType={marketTypeValue}
        [Authorize(Roles = "Admin")]
        public List<BetDTOLessInfoA> GetBetByEmailAndMarketType(string email, float marketType)
        {
            var repository = new BetRepository();
            List<BetDTOLessInfoA> bets = repository.RetrieveByEmailAndMarketType(email, marketType);
            return bets;
        }

        //localhost:44346/api/Bet?marketID={marketIDvalue}&email={emailValue}
        [Authorize(Roles = "Admin")]
        public List<BetDTOLessInfoB> GetBetByEmailAndMarketID(int marketID, string email)
        {
            var repository = new BetRepository();
            List<BetDTOLessInfoB> bets = repository.RetrieveByEmailAndMarketID(marketID, email);
            return bets;
        }
    }
}
