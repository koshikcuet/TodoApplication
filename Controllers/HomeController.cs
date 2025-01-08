using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NishuPortFolio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace NishuPortFolio.Controllers
{

   

    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

     
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("PortfolioDB");
        }

        public IActionResult Index()
        {
            ViewData["ActiveMenu"] = "Home";
            List<Content> contentList = GetContent1(ant: 1); // Adjust the ant parameter as needed
            string title = contentList.FirstOrDefault()?.Title; // Get the title from the first content item
            ViewData["Title"] = title; // Store title in ViewData

            return View();

        }


        public List<Content> GetContent(int ant)
        {
            List<Content> contents = new List<Content>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Start building the query
                string query = "SELECT * FROM [ent_content] WHERE ant = @Ant"; // Only filter by ant (Id)

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter for ant to avoid SQL injection
                    cmd.Parameters.AddWithValue("@Ant", ant);

                    conn.Open(); // Open the database connection

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Iterate through the result set
                        while (reader.Read())
                        {
                            Content content = new Content
                            {
                                Id = Convert.ToInt32(reader["ant"]), // Map the 'ant' column to Id
                                Title = reader["title"].ToString(), // Map the 'title' column to Title
                                Detail = reader["detail"].ToString() // Map the 'detail' column to Detail
                            };

                            contents.Add(content); // Add the content to the list
                        }
                    }
                }
            }

            return contents; // Return the filtered list of content
        }



        public List<Content> GetContent1(int ant)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Query to retrieve content filtered by ant
                string query = "SELECT * FROM [ent_content] WHERE ant = @Ant";

                // Execute the query and map the results to a list of Content objects
                return conn.Query<Content>(query, new { Ant = ant }).ToList();

            }
        }

        public List<list_project> GetProject()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Query to retrieve content filtered by ant
                string query = "SELECT * FROM [list_project]";

                // Execute the query and map the results to a list of Content objects
                return conn.Query<list_project>(query).ToList();

            }
        }



        public IActionResult ProjectDetails()
        {
            ViewData["ActiveMenu"] = "Projects";

           return View();

        }
        public IActionResult Contact()
        {
            ViewData["ActiveMenu"] = "Contact";
            return View();   


        }
        
        public IActionResult Research()
        {
            ViewData["ActiveMenu"] = "Research";
            return View();
        }
        public IActionResult Projects()
        {
            ViewData["ActiveMenu"] = "Projects";
            List<list_project> contentList = GetProject(); // Adjust the ant parameter as needed
            return View(contentList); // Pass the data to the view
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["ActiveMenu"] = "About";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}
