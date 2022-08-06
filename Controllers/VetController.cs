using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

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
            user.RoyaltyVets = RoyaltyVetRating();
            user.PetVetClub = PetVetClubRating();
            user.CareVeterinary = CareVeterinaryRating();
            user.PetVetClinic = PetVetClinicRating();

            return View(user);
        }

        public int RoyaltyVetRating()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT AVG(VetRating) FROM Vet WHERE VetName = \'royaltyvets\'";
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

        public int PetVetClubRating()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT AVG(VetRating) FROM Vet WHERE VetName = \'petvetclub\'";
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

        public int CareVeterinaryRating()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT AVG(VetRating) FROM Vet WHERE VetName = \'careveterinary\'";
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

        public int PetVetClinicRating()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT AVG(VetRating) FROM Vet WHERE VetName = \'petvetclinic\'";
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
            return View("~/Views/Home/EmailSent.cshtml", user);
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
            return View("~/Views/Home/EmailSent.cshtml", user);
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
            return View("~/Views/Home/EmailSent.cshtml", user);
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
            return View("~/Views/Home/EmailSent.cshtml", user);
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
            using (SqlConnection connection = new SqlConnection
                (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;
                Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
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
            using (SqlConnection connection = new SqlConnection
                (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;
                Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
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
            using (SqlConnection connection = new SqlConnection
                (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;
                Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
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
            using (SqlConnection connection = new SqlConnection
                (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;
                Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
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