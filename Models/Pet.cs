using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Pet
    {
        public Pet()
        {

        }
        public Pet(int ownerId, int id, string petName, string petBreed, bool firstVacc, DateTime firstVaccDate, bool secondVacc, DateTime secondVaccDate)
        {
            OwnerId = ownerId;
            Id = id;
            PetName = petName;
            PetBreed = petBreed;
            FirstVaccination = firstVacc;
            FirstVaccinationDate = firstVaccDate;
            SecondVaccination = secondVacc;
            SecondVaccinationDate= secondVaccDate;
        }

        public int OwnerId { get; set; }
        public int Id { get; set; }
        public string PetName { get; set; }
        public string PetBreed { get; set; }

        public bool FirstVaccination { get; set; }

        public DateTime FirstVaccinationDate { get; set; }

        public bool SecondVaccination { get; set; }

        public DateTime SecondVaccinationDate { get; set; }

        public int Count { get; set; }

    }
}
