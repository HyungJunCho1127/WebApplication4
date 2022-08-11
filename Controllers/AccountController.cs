using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AccountController : Controller
    {

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Models.User user = new Models.User();
            try
            {
                ViewData["UserName"] = collection[1];
                ViewData["Email"] = collection[2];
                ViewData["Password"] = collection[3];

                user.UserName = collection[1];
                user.Email = collection[2];
                user.Password = collection[3];

                String username = collection[1];
                String email = collection[2];
                String password = collection[3];
                Connection connectionString = new Connection();
                
                using (SqlConnection connection = new SqlConnection(connectionString.GetConnection()))
                {
                    // using parameters add with value to protect against SQL injection
                    String query = "INSERT INTO Account (Username,Password, Email, UserType) VALUES (@Username,@Password, @Email, @UserType)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@UserType", "User");
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }
                int userId = GetUserID(username, password);
                user.Id = userId;
                user.List = new List<PetModel>();
                return View("~/Views/Home/Index.cshtml", user);
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Create
        public ActionResult Login(FormCollection collection)
        {
            Models.User user = new Models.User();
            ViewResult view = new ViewResult();
            try
            {
                ViewData["UserName"] = collection[1];
                ViewData["Password"] = collection[2];
                string username = collection[1];
                string password = collection[2];
                Connection connectionString = new Connection();
                SqlConnection connection = new SqlConnection(connectionString.GetConnection());
                string query = "SELECT * FROM Account WHERE Username = @username AND Password = @password";
                // using parameters add with value to protect against SQL injection
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (username == reader[1].ToString() && password == reader[2].ToString())
                        {
                            if (reader[4].ToString() == "User")
                            {
                                user.Id = int.Parse(reader[0].ToString());
                                user.UserName = collection[1];
                                user.Email = reader[3].ToString();
                                user.List = GetPetsList(user.Id);
                                view = View("~/Views/Home/Index.cshtml", user);
                            }
                            if (reader[4].ToString() == "Admin")
                            {
                                user.Id = int.Parse(reader[0].ToString());
                                user.UserName = collection[1];
                                user.Email = reader[3].ToString();
                                user.UserList = GetUserList();
                                view = View("~/Views/Admin/Index.cshtml", user);
                            }
                        } else
                        {
                        }
                    }
                    else
                    {
                        return View("LoginFailed");
                    }
                }
                catch
                {
                }
                return view;
            }
            catch
            {
                // this one doesn't affect
                return View();
            }
        }

        public ActionResult LoginFailed()
        {
            return View("LoginFailed");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Password(string userName)
        {
            string email = "";
            string password = "";
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());

            string query = "SELECT * FROM Account WHERE Username = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", userName);
            SqlDataReader reader;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (userName == reader[1].ToString())
                    {
                        password = reader[2].ToString();
                        email = reader[3].ToString();
                    }
                    else
                    {
                    }
                }
                else
                {
                }

            }
            catch
            {

            }
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ito5032email@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Password Reset for PetsaGram";
                mail.Body = "<h1>Your Password is '" + password + "' + </h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ito5032email@gmail.com", "namdbcfxkskxusht");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return View("ForgotPasswordSent");
        }

        public int GetUserID(string userName, string password)
        {
            int userId = 0;
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());

            string query = "SELECT * FROM Account WHERE Username = @username AND Password = @password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", userName);
            command.Parameters.AddWithValue("@password", password);
            SqlDataReader reader;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (userName == reader[1].ToString() && password == reader[2].ToString())
                    {
                        userId = int.Parse(reader[0].ToString());
                    }
                    else
                    {
                    }
                }
                else
                {
                }
                return userId;
            } catch
            {
                return userId;
            }
        }

        public List<PetModel> GetPetsList(int id)
        {
            string name = " ";
            string breed = " ";
            string vacc = "";
            string date = "";
            List<PetModel> list = new List<PetModel>();
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());
            String query = "SELECT * FROM Pets WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    { 
                        name = reader["PetName"].ToString();
                        breed = reader["PetBreed"].ToString();
                        if (reader["FirstVaccination"].ToString() == "No")
                        {
                            vacc = "";
                            date = "";
                        } else
                        {
                            vacc = reader["FirstVaccination"].ToString();
                            date = reader["FirstVaccinationDate"].ToString();
                         
                        }

                        PetModel details = new PetModel(name, breed, vacc, date);
                        list.Add(details);
                    }

                }
                catch
                {
                }
            }
            return list;
        }

        public List<UserModel> GetUserList()
        {
            string id = " ";
            string userName = " ";
            string email = "";
            List<UserModel> list = new List<UserModel>();
            Connection connectionString = new Connection();
            SqlConnection connection = new SqlConnection(connectionString.GetConnection());
            String query = "SELECT * FROM Account WHERE UserType = \'User\'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id = reader["Id"].ToString();
                        userName = reader["Username"].ToString();
                        email = reader["Email"].ToString();

                        UserModel details = new UserModel(id, userName, email);
                        list.Add(details);
                    }

                }
                catch
                {
                }
            }
            return list;
        }
    } 
}