using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.IO;
using WebApplication4.Models;
using Image = WebApplication4.Models.Image;
using System.Net.Mail;
using System.Net;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
        
        public ActionResult Index(Models.User user)
        {
            AccountController account = new AccountController();
            user.List = account.GetPetsList(user.Id);
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
            string newfileName = Path.Combine(Server.MapPath("~/Image/"), fileName + ".png");
            image.ImageFile.SaveAs(newfileName);
            Connection connectionString = new Connection();
            using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
            {
                // using parameters add with value to protect against SQL injection
                String query = "INSERT INTO Image (Id, Image1) VALUES (@Id, @Image1)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Image1", fileName + ".png");
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
            return View("FileUploaded", user);
        }
        

        
    } 

}