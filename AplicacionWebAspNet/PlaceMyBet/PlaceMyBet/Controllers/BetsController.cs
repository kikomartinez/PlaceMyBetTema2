using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class BetsController : ApiController
    {
        // GET: api/Bets
        public IEnumerable<Bets> Get()
        {
            var repository = new BetsRepository();
            List<Bets> bets = repository.Retrieve();
            return bets;
        }

        // GET: api/Bets/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bets
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bets/5
        public void Delete(int id)
        {
        }
    }
}
