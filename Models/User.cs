using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class User
    {
        public User()
        {

        }
        public User(int id, string userName, string password, string email, string userType)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Email = email;
            UserType = userType;
        }


        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string UserType { get; set; }

    }
}