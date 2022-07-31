using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ViewModel
    {

        public ViewModel(User getUser, Pet getPet)
        {
            GetUser = getUser;
            GetPet = getPet;
        }

        public User GetUser { get; set; }

        public Pet GetPet { get; set; }
    }
}