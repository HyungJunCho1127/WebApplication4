using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class PetModel
    {
        public string PetName { get; set; }
        public string PetBreed { get; set; }

        public string Vaccination { get; set; }

        public string VaccinationDate { get; set; }

        public PetModel()
        {

        }
        public PetModel(string petName, string petBreed, string vaccination, string vaccinationDate)
        {
            PetName = petName;
            PetBreed = petBreed;
            Vaccination = vaccination;
            VaccinationDate = vaccinationDate;
        }

    }
}