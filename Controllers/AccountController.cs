﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
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
                                view = View("~/Views/Admin/Index.cshtml", user);
                            }

                        } else
                        {
                        }
      
                    }
                    else
                    {
                        // THIS ONE!!!!!
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

        public List<PetModel> GetPetsList(int id)
        {
            string name = " ";
            string breed = " ";
            string vacc = "";
            string date = "";
            List<PetModel> list = new List<PetModel>();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            String query = "SELECT * FROM Pets WHERE Id = \'" + id + "\'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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
    } 
}