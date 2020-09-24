using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public abstract class AbstractController<TRepository, T> : ApiController where TRepository : AbstractRepository<T>, new() where T : class
    {
        // GET: api/Abstract
        public IEnumerable<T> Get() 
        {
            var repository = new TRepository();
            List<T> itemsToGet = repository.Retrieve();
            return itemsToGet;
        }

    }
}
