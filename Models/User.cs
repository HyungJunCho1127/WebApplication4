using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Models
{
    public class User
    {

        public User()
        {

        }
        public User(int id, string userName, string password, string email)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Email = email;

        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //
        public string EmailText { get; set; }

        public string UserType { get; set; }


        // vets 

        public int RoyaltyVets { get; set; }

        public int PetVetClub { get; set; }

        public int CareVeterinary { get; set; }

        public int PetVetClinic { get; set; }

        public int VetRating { get; set; }

        // pets 
        public int OwnerId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetBreed { get; set; }
        public SelectList PetSelectList { get; set; }
        public bool FirstVaccination { get; set; }

        public DateTime FirstVaccinationDate { get; set; }

        public int Count { get; set; }

        public PieChartVM PieChartData { get; set; }

        public List<PetModel> List { get; set; }
    }
}