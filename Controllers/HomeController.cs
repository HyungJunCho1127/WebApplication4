using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using WebApplication4.Models;
using Image = WebApplication4.Models.Image;
using System.Web.Helpers;
using System.Net.Mail;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Models.User user)
        {
            return View(user); ;
        }

        // GET: Home/Create
        public ActionResult Email(Models.User user)
        {
            return View(user);
        }
        // POST: Home/Create
        [HttpPost]
        public ActionResult SendEmail(Models.User user)
        {
                    using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Vaccination Document Form";
                mail.Body = "<h1>Please find attached vaccination form</h1>";
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new Attachment("C:/Users/davch/source/repos/WebApplication4/Image/form.png"));
                //    mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("EmailSent", user);
        }

        public ActionResult EmailSent(Models.User user)
        {
            return View(user);
        }

        [HttpGet]
        public ActionResult Upload(Models.User user)
        {
            return View(user);
        }

        public ActionResult FileUpload(Models.User user, Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            image.Image1 = "../Image/" + fileName;
            image.Id = user.Id;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName + ".png");
            image.ImageFile.SaveAs(fileName);
            using (Model1 model1 = new Model1())
            {
                model1.Images.Add(image);
                model1.SaveChanges();
            }
            ModelState.Clear();
            return View("FileUploaded", user);
        }
        public ActionResult AddPets()
        {
            return View(); ;
        }
        public ActionResult PetAdd(Models.User user, FormCollection collection)
        {
            Models.Pet pet = new Models.Pet();
            try
            {
                ViewData["PetName"] = collection[0];
                ViewData["PetBreed"] = collection[1];
                string petName = collection[0];
                string petBreed = collection[1];
                pet.PetName = petName;
                pet.PetBreed = petBreed;
                int userId = user.Id;
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
                    String query = "INSERT INTO Pets (Id,PetName, PetBreed) VALUES (@Id,@PetName, @PetBreed)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userId);
                        command.Parameters.AddWithValue("@PetName", petName);
                        command.Parameters.AddWithValue("@PetBreed", petBreed);
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }
                return View("PetAdded", user);
            }
            catch
            {
                return View();
            }

        }
        public ActionResult PetAdded(Models.User user)
        {
            return View("PetAdded", user);
        }
        public ActionResult PieChart()
        {
            var masterModel = new HomeIndexVM();
            // fill pie chart with info
            var pieChartData = GetPieChartData();
            masterModel.PieChartData = pieChartData;
            return View(masterModel);
        }

        // get the data for the pie chart
        private PieChartVM GetPieChartData()
        {
            PieChartVM model = new PieChartVM();
            List<string> labels = new List<string>();
            labels.Add("Pomeranian");
            labels.Add("Bulldog");
            labels.Add("British Shorthair");
            labels.Add("Siamese");
            labels.Add("German Shepherd");
            labels.Add("Golden Retriever");
            labels.Add("Poodle");
            labels.Add("Husky");
            labels.Add("Pug");
            model.labels = labels;
            List<PieChartChildVM> datasets = new List<PieChartChildVM>();
            PieChartChildVM piechart = new PieChartChildVM();
            List<string> backgroundColorList = new List<string>();
            List<int> dataList = new List<int>();
                    foreach (string label in labels)
            {
                if (label == "Pomeranian")
                {
                    backgroundColorList.Add("#cc882e");
                    dataList.Add(GetPomeranianDataCount());
                }
                if (label == "Bulldog")
                {
                    backgroundColorList.Add("#636968");
                    dataList.Add(GetBulldogDataCount());
                }
                if (label == "British Shorthair")
                {
                    backgroundColorList.Add("#e3e1a8");
                    dataList.Add(GetShortHairDataCount());
                }
                if (label == "Siamese")
                {
                    backgroundColorList.Add("#cca8e3");
                    dataList.Add(GetSiameseDataCount());
                }
                if (label == "German Shepherd")
                {
                    backgroundColorList.Add("#4B5320");
                    dataList.Add(GetShepardDataCount());
                }
                if (label == "Golden Retriever")
                {
                    backgroundColorList.Add("#583759");
                    dataList.Add(GetRetrieverDataCount());
                }
                if (label == "Poodle")
                {
                    backgroundColorList.Add("#FF6700");
                    dataList.Add(GetPoodleDataCount());
                }
                if (label == "Husky")
                {
                    backgroundColorList.Add("#3D3635");
                    dataList.Add(GetHuskyDataCount());
                }
                if (label == "Pug")
                {
                    backgroundColorList.Add("#827B60");
                    dataList.Add(GetPugDataCount());
                }
            }
            piechart.backgroundColor = backgroundColorList;
            piechart.data = dataList;
            datasets.Add(piechart);
            model.datasets = datasets;
            return model;
        }

        public int GetPomeranianDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Pomeranian\'";

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

        public int GetBulldogDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");

            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Bulldog\'";

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

        public int GetShortHairDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'British Shorthair\'";
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

        public int GetSiameseDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Siamese\'";
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

        public int GetShepardDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'German Shepherd\'";

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
        public int GetRetrieverDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Golden Retriever\'";

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

        public int GetPoodleDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");

            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Poodle\'";
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

        public int GetHuskyDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Husky\'";
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
        public int GetPugDataCount()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'Pug\'";
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
    } 
}