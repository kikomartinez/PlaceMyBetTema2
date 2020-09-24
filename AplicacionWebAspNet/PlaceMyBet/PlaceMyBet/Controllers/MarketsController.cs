using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class MarketsController : ApiController
    {
        // GET: api/Markets
        public IEnumerable<Markets> Get()
        {
            var repository = new MarketsRepository();
            List<Markets> markets = repository.Retrieve();
            return markets;
        }

        // GET: api/Markets/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Markets
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Markets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Markets/5
        public void Delete(int id)
        {
        }
    }
}
