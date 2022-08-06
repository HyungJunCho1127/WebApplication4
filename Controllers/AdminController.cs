using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(Models.User user)
        {

            return View(user);
        }

        public ActionResult AdminEmail(Models.User user)
        {
            return View(user);
        }

        public ActionResult SendAllUsersEmail(string emailBody, Models.User user)
        {
            string[] email = GetAllUserEmail();
            for (int i = 0; i < GetAllUserEmail().Length; i++)
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ito5032email@gmail.com");
                    // put recepitent email here

                    mail.To.Add(email[i]);

                    mail.Subject = "Email from PetsaGram";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    mail.Attachments.Add(new Attachment("C:/Users/davch/source/repos/WebApplication4/Image/form.png"));
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            
            return View("EmailSent", user);
        }

        public string[] GetAllUserEmail()
        {
            string[] email = new string[GetAllUserEmailCount()];
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());
            String query = "SELECT Email FROM Account WHERE UserType = \'User\'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                         email[i] = reader["Email"].ToString();
                        i++;
                    }
                }
                catch
                {
                }
            }

            return email;
        }

        public int GetAllUserEmailCount()
        {
            int count = 0;
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());       
            String query = "SELECT COUNT(Email) FROM Account WHERE UserType = \'User\'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        count = int.Parse(reader[0].ToString());
                    }
                }
                catch
                {
                }
            }
            return count;
        }

        public ActionResult EmailSent(Models.User user)
        {
            return View(user);
        }

        public ActionResult UserList(Models.User user)
        {
            AccountController account = new AccountController();
            user.UserList = account.GetUserList();
            return View(user);
        }
    }
}