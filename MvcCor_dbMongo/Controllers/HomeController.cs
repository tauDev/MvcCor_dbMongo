using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MvcCor_dbMongo.Models;

namespace MvcCor_dbMongo.Controllers
{
    public class HomeController : Controller
    {
        private IMongoDatabase mongDB;
        public IMongoDatabase GetMongoDatabase()
        {
            var mongClients = new MongoClient("mongodb://localhost:27017");
            return mongClients.GetDatabase("local");
        }
        public IActionResult Index()
        {
            mongDB = GetMongoDatabase();
            var result = mongDB.GetCollection<User>("test").Find(FilterDefinition<User>.Empty).ToList();
            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
