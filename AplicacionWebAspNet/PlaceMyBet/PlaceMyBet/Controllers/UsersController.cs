using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<Users> Get()
        {
            var repository = new UsersRepository();
            List<Users> users = repository.Retrieve();
            return users;
        }

        // GET: api/Users/5
        public Users Get(int id)
        {
            return null;
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
