using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class UsersRepository
    {
        internal Users Retrieve()
        {
            Users user = new Users("prueba@gmail.com", "Pepe", "Martínez", 25);

            return user;
        }
    }
}