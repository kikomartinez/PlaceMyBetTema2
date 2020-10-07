using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class BetController : AbstractController<BetRepository, BetDTO>
    {
        public void Post([FromBody]Bet bet)
        {
            var repository = new BetRepository();
            repository.Save(bet);
        }
    }
}
