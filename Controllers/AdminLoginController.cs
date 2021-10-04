using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using EasyManage.Models;

namespace EasyManage.Controllers
{
    public class AdminLoginController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            return View();
        }




        /*
        [HttpPost]
        public IActionResult Create(adminLogin admin)
        {

            var database = client.GetDatabase("EasyManage");
            var table = database.GetCollection<adminLogin>("admin");
            admin.Id = Guid.NewGuid().ToString();

            table.InsertOne(student);
            // ViewBag.msg = "Student has been saved";

            //return View();

            return RedirectToAction("Index");
        }
        */


    }
}
