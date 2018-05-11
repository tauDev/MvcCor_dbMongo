using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MvcCor_dbMongo.Models;

namespace MvcCor_dbMongo.Controllers
{
    public class HomeController : Controller
    {
        private IMongoDatabase mongDB;
        public IMongoDatabase GetMongoDatabase()
        {
            var mongClients = new MongoClient("mongodb://192.168.0.124:27017");
            return mongClients.GetDatabase("local");
        }
        public IActionResult Index()
        {
            try
            {
                mongDB = GetMongoDatabase();
                var collection = mongDB.GetCollection<User>("test");
                var result = mongDB.GetCollection<User>("test").Find(FilterDefinition<User>.Empty).ToList();
                return View(result);
            }
            catch (Exception)
            {
                //throw;
                return View();
            }
           
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
        public IActionResult Create()
        {
            mongDB = GetMongoDatabase();
            // var collection = mongDB.GetCollection<User>("test");
            try
            {
                ViewBag.CountRow = mongDB.GetCollection<User>("test").Find(b => true).Count();
            }
            catch(Exception)
            {
                ViewBag.CountRow = 0;
            }            
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                mongDB = GetMongoDatabase();
              
                mongDB.GetCollection<User>("test").InsertOne(user);
            }catch (Exception)
            {
               // throw;
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            mongDB = GetMongoDatabase();

            var user = mongDB.GetCollection<User>("test").DeleteOne<User>(k => k.No == id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(User dd)
        {
            try
            {
                mongDB = GetMongoDatabase();
                var result = mongDB.GetCollection<User>("test").DeleteOne<User>(k => k.No == dd.No);
                if (result.IsAcknowledged == false)
                {
                    return BadRequest("Unable to Delete User " + dd.No);
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            mongDB = GetMongoDatabase();

            var user = mongDB.GetCollection<User>("test").Find<User>(k => k.No == id).FirstOrDefault();
            return View(user);
        }
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            mongDB = GetMongoDatabase();
            var users = mongDB.GetCollection<User>("test").Find<User>(a => a.No == id).FirstOrDefault();
            if(users==null)
            {
                return NotFound();
            }
           
            return View(users);
        }
        [HttpPost]
        public IActionResult Edit(User users)
        {
            try
            {
                mongDB = GetMongoDatabase();
                var filter = Builders<User>.Filter.Eq("No", users.No);
                var updatestatement = Builders<User>.Update.Set("No", users.No);
                updatestatement = updatestatement.Set("Uname", users.Uname);
                updatestatement = updatestatement.Set("Password", users.Password);
                var result = mongDB.GetCollection<User>("test").UpdateOne(filter, updatestatement);
                if(result.IsAcknowledged==false)
                {
                    return BadRequest("Unable to update Customer " + users.Uname);
                }
            }catch(Exception)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
