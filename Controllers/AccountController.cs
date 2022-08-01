using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebApplication4.Controllers
{
    public class AccountController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

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

                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                        C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;Multipl
                            eActiveResultSets=True;Application Name=EntityFramework"))
                {
                    String query = "INSERT INTO Account (Username,Password, Email) VALUES (@Username,@Password, @Email)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }
                int userId = GetUserID(username, password);
                user.Id = userId;
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

                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                        C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
                            MultipleActiveResultSets=True;Application Name=EntityFramework");

                string query = "";
                query = "SELECT * FROM Account WHERE Username = '" + username + "' AND Password = '" + password + "'";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (username == reader[1].ToString() && password == reader[2].ToString())
                        {
                            user.Id = int.Parse(reader[0].ToString());
                            user.UserName = collection[1];
                            user.Password = collection[2];
                            user.Email = reader[3].ToString();
                            HomeController home = new HomeController();
                            Models.ViewModel viewModel = new Models.ViewModel(user, null);
                            view = View("~/Views/Home/Index.cshtml", user);

                        } else
                        {
                        }
                    }
                    else
                    {
                        // THIS ONE!!!!!
                        view = View("LoginFailed");
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

        public int GetUserID(string userName, string password)
        {
            int userId = 0;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
                MultipleActiveResultSets=True;Application Name=EntityFramework");
            string query = "SELECT * FROM Account WHERE Username = '" + userName + "' AND Password = '" + password + "'";
            SqlCommand command = new SqlCommand(query, connection);
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
    } 
}