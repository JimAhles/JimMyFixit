using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFixIt.Models;
using MyFixIt.Logging;

namespace MyFixIt.Controllers
{
    public class DogController : Controller
    {
        // GET: Dog

        private ILogger log;

        public DogController( ILogger log)
        {
            this.log = log;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            Dog dog = new Dog
            {
                Id = 7,
                Name = "Lord woofington",
                BirthDate = new DateTime(year: 2005, month: 06, day: 27)
            };
            return View(dog);
        }

        public string Bark()
        {
            return "Woof!";
        }

        [HttpGet]

        public JsonResult CatDog()
        {
            // some code
            return Json(new { success = true, message = "Meow! Bark!" }, JsonRequestBehavior.AllowGet);
        }

    }
}