using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;

using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    public class PetController : Controller
    {
        // GET: Pet
        public ActionResult Index(Models.User user)
        {
            return View(user); ;
        }

        public ActionResult AddPets(Models.User user)
        {
            return View(user);
        }
        public ActionResult PetAdd(string petName, string petBreed,string vaccination,
            string year, string month, string day, Models.User user)
        {
            try
            {
                int userId = int.Parse(user.Id.ToString());
                using (SqlConnection connection = new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                    C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;
                    Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
                    if (vaccination == "Yes")
                    {
                        DateTime date = SetDates(year, month, day);
                        String query = "INSERT INTO Pets (Id,PetName, PetBreed, FirstVaccination, FirstVaccinationDate)" +
                            " VALUES (@Id,@PetName, @PetBreed, @FirstVaccination, @FirstVaccinationDate)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", userId);
                            command.Parameters.AddWithValue("@PetName", petName);
                            command.Parameters.AddWithValue("@PetBreed", petBreed);
                            command.Parameters.AddWithValue("@FirstVaccination", vaccination);
                            command.Parameters.AddWithValue("@FirstVaccinationDate", date);
                            connection.Open();
                            int result = command.ExecuteNonQuery();
                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                        }
                    }

                    if (vaccination == "No")
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

                }
                return View("PetAdded", user);
            }
            catch
            {
                return View();
            }

        }

        public DateTime SetDates(string year, string month, string day)
        {
            DateTime date = new DateTime();
            if (month == "January")
            {
                month = "01";
            }
            if (month == "February")
            {
                month = "02";
            }
            if (month == "March")
            {
                month = "03";
            }
            if (month == "April")
            {
                month = "04";
            }
            if (month == "May")
            {
                month = "05";
            }
            if (month == "June")
            {
                month = "06";
            }
            if (month == "July")
            {
                month = "07";
            }
            if (month == "August")
            {
                month = "08";
            }
            if (month == "September")
            {
                month = "09";
            }
            if (month == "October")
            {
                month = "10";
            }
            if (month == "November")
            {
                month = "11";
            }
            if (month == "December")
            {
                month = "12";
            }

            date = DateTime.Parse(year + "-" + month + "-" + day);

            return date;
        }
        public ActionResult PetAdded(Models.User user)
        {
            return View("PetAdded", user);
        }

        public ActionResult Vaccination(Models.User user)
        {
            user.PetSelectList = new SelectList("String", "test", "mark");
            return View(user);
        }

        public ActionResult AddVaccination(string year, string month, string day, Models.User user)
        {
            return View(user);
        }

        public ActionResult BarChart(Models.User user)
        {
            // fill pie chart with info
            var pieChartData = GetPieChartData();
            user.PieChartData = pieChartData;
            return View(user);
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
            labels.Add("Sphynx");
            labels.Add("Munchkin");
            labels.Add("Chihuahua");
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
                    dataList.Add(GetDataCount("Pomeranian"));
                }
                if (label == "Bulldog")
                {
                    backgroundColorList.Add("#636968");
                    dataList.Add(GetDataCount("Bulldog"));
                }
                if (label == "British Shorthair")
                {
                    backgroundColorList.Add("#e3e1a8");
                    dataList.Add(GetDataCount("British Shorthair"));
                }
                if (label == "Siamese")
                {
                    backgroundColorList.Add("#cca8e3");
                    dataList.Add(GetDataCount("Siamese"));
                }
                if (label == "German Shepherd")
                {
                    backgroundColorList.Add("#4B5320");
                    dataList.Add(GetDataCount("German Shepherd"));
                }
                if (label == "Golden Retriever")
                {
                    backgroundColorList.Add("#583759");
                    dataList.Add(GetDataCount("Golden Retriever"));
                }
                if (label == "Poodle")
                {
                    backgroundColorList.Add("#FF6700");
                    dataList.Add(GetDataCount("Poodle"));
                }
                if (label == "Husky")
                {
                    backgroundColorList.Add("#3D3635");
                    dataList.Add(GetDataCount("Husky"));
                }
                if (label == "Pug")
                {
                    backgroundColorList.Add("#827B60");
                    dataList.Add(GetDataCount("Pug"));
                }
                if (label == "Sphynx")
                {
                    backgroundColorList.Add("#FF3380");
                    dataList.Add(GetDataCount("Sphynx"));
                }
                if (label == "Munchkin")
                {
                    backgroundColorList.Add("#FFFC33");
                    dataList.Add(GetDataCount("Munchkin"));
                }
                if (label == "Chihuahua")
                {
                    backgroundColorList.Add("#FF3333");
                    dataList.Add(GetDataCount("Chihuahua"));
                }
            }
            piechart.backgroundColor = backgroundColorList;
            piechart.data = dataList;
            datasets.Add(piechart);
            model.datasets = datasets;
            return model;
        }

        public int GetDataCount(string breed)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)
            \MSSQLLocalDB;AttachDbFilename=C:\Users\davch\source\repos\WebApplication4\
            App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;
            Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(PetBreed) FROM Pets WHERE PetBreed = \'"+ breed + "\'";

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

        public ActionResult PieChart(Models.User user)
        {
            // fill pie chart with info
            var chartData = GetChartData();
            user.PieChartData = chartData;
            return View(user);
        }

        private PieChartVM GetChartData()
        {
            PieChartVM model = new PieChartVM();
            List<string> labels = new List<string>();
            labels.Add("Vaccinated");
            labels.Add("Not Vaccinated");
            model.labels = labels;
            List<PieChartChildVM> datasets = new List<PieChartChildVM>();
            PieChartChildVM piechart = new PieChartChildVM();
            List<string> backgroundColorList = new List<string>();
            List<int> dataList = new List<int>();
            foreach (string label in labels)
            {
                if (label == "Vaccinated")
                {
                    backgroundColorList.Add("#76E767");
                    dataList.Add(GetVaccinationCount("Yes"));
                }
                if (label == "Not Vaccinated")
                {
                    backgroundColorList.Add("#F38787");
                    dataList.Add(GetVaccinationCount("No"));
                }
            }
            piechart.backgroundColor = backgroundColorList;
            piechart.data = dataList;
            datasets.Add(piechart);
            model.datasets = datasets;
            return model;
        }

        public int GetVaccinationCount(string word)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework");
            int number = 0;
            String query = "SELECT COUNT(FirstVaccination) FROM Pets WHERE FirstVaccination = \'"+ word + "\'";
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