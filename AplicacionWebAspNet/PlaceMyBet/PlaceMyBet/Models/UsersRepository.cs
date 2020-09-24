using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class UsersRepository : AbstractRepository<Users>
    {
        public UsersRepository()
        {
            tableName = "USUARIOS";
        }

        protected override Users ConvertInfoToObject()
        {
            Users user = new Users(result.GetString(0), result.GetString(1), result.GetString(2), result.GetInt32(3));
            return user;
        }
    }
}