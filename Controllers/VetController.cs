using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class VetController : Controller
    {
        // GET: Vet
        public ActionResult Index(Models.User user)
        {
            return View(user); ;
        }

        public ActionResult Vet(Models.User user)
        {
            user.RoyaltyVets = VetRating("royaltyvets");
            user.PetVetClub = VetRating("petvetclub");
            user.CareVeterinary = VetRating("careveterinary");
            user.PetVetClinic = VetRating("petvetclinic");

            return View(user);
        }

        public int VetRating(string vet)
        {
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());
            
            int number = 0;
            //no need for parameters are declared.
            String query = "SELECT AVG(VetRating) FROM Vet WHERE VetName = \'"+ vet + "\'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        number = int.Parse(reader[0].ToString());
                    }
                }
                catch
                {
                }
            }
            return number;
        }

        public ActionResult BookRoyaltyVets(Models.User user)
        {
            return View(user);
        }

        public ActionResult BookRoyaltyVetEmail(string emailBody, Models.User user)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                // put recepitent email here
                mail.To.Add("junenetjourney@gmail.com");
                mail.Subject = "Email For Royalty vets";
                mail.Body = "<h1> '" + emailBody + "'</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("EmailSent", user);
        }

        public ActionResult BookPetVetClub(Models.User user)
        {
            return View(user);
        }

        public ActionResult BookPetVetClubEmail(string emailBody, Models.User user)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                // put recepitent email here
                mail.To.Add("junenetjourney@gmail.com");
                mail.Subject = "Email for Pet Vet Club";
                mail.Body = "<h1> '" + emailBody + "'</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("EmailSent", user);
        }

        public ActionResult BookPetVetClinic(Models.User user)
        {
            return View(user);
        }

        public ActionResult BookPetVetClinicEmail(string emailBody, Models.User user)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                // put recepitent email here
                mail.To.Add("junenetjourney@gmail.com");
                mail.Subject = "Email for Pet Vet Clinic";
                mail.Body = "<h1> '" + emailBody + "'</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("EmailSent", user);
        }

        public ActionResult BookCareVeterinary(Models.User user)
        {
            return View(user);
        }

        public ActionResult BookCareVeterinaryEmail(string emailBody, Models.User user)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                // put recepitent email here
                mail.To.Add("junenetjourney@gmail.com");
                mail.Subject = "Email for Care Veterinary";
                mail.Body = "<h1> '" + emailBody + "'</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("EmailSent", user);
        }


        public ActionResult RatingDone(Models.User user)
        {
            return View(user);
        }
        
        public ActionResult RateRoyaltyVets(Models.User user)
        {
            return View(user);
        }
        public ActionResult RoyaltyVetsRate(string rating, Models.User user)
        {
            int vetRating = int.Parse(rating);
            Connection connectionString = new Connection();
            using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
            {
                String query = "INSERT INTO Vet (VetName,VetRating) VALUES (@VetName,@VetRating)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VetName", "royaltyvets");
                    command.Parameters.AddWithValue("@VetRating", vetRating);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
            return View("RatingDone",user);
        }

        public ActionResult RateCareVeterinary(Models.User user)
        {
            return View(user);
        }
        public ActionResult CareVeterinaryRate(string rating, Models.User user)
        {
            int vetRating = int.Parse(rating);
            Connection connectionString = new Connection();
            using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
            {
                String query = "INSERT INTO Vet (VetName,VetRating) VALUES (@VetName,@VetRating)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VetName", "careveterinary");
                    command.Parameters.AddWithValue("@VetRating", vetRating);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
            return View("RatingDone", user);
        }

        public ActionResult RatePetVetClinic(Models.User user)
        {
            return View(user);
        }

        public ActionResult PetVetClinicRate(string rating, Models.User user)
        {
            int vetRating = int.Parse(rating);
            Connection connectionString = new Connection();
            using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
            {
                String query = "INSERT INTO Vet (VetName,VetRating) VALUES (@VetName,@VetRating)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VetName", "petvetclinic");
                    command.Parameters.AddWithValue("@VetRating", vetRating);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
            return View("RatingDone", user);
        }

        public ActionResult RatePetVetClub(Models.User user)
        {
            return View(user);
        }

        public ActionResult PetVetClubRate(string rating, Models.User user)
        {
            int vetRating = int.Parse(rating);
            Connection connectionString = new Connection();
            using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
            {
                String query = "INSERT INTO Vet (VetName,VetRating) VALUES (@VetName,@VetRating)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VetName", "petvetclub");
                    command.Parameters.AddWithValue("@VetRating", vetRating);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
            return View("RatingDone", user);
        }

    }
}