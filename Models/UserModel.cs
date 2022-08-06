using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }


        public UserModel()
        {

        }
        public UserModel(string id, string username, string email)
        {
            Id = id;
            UserName = username;
            Email = email;

        }

    }
}
