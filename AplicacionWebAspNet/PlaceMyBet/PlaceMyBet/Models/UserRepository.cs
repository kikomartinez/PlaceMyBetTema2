using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository()
        {
            tableName = "USUARIOS";
        }

        protected override User ConvertInfoToObject()
        {
            User user = new User(result.GetString(0), result.GetString(1), result.GetString(2), result.GetInt32(3));
            return user;
        }
    }
}