using EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.EM;

namespace ZeroHunger.Controllers
{
    
    public class FoodController : Controller
    {
        
        [HttpGet]
        public ActionResult CreateFood()
        {
            var db = new ZeroHungerEntities();
            var id = Session["id"];
            var res = db.Restaurants.Find(id);
            return View(res);
        }
        [RestaurantPermission]
        [HttpPost]
        public ActionResult CreateFood(Food f)
        {
            var db = new ZeroHungerEntities();
            f.status = "None";
            db.Foods.Add(f);
            db.SaveChanges();

            return RedirectToAction("Home","Home");

        }


        [EmployeePermission]
        [HttpGet]
        public ActionResult EditFood(int id)
        {
            var db = new ZeroHungerEntities();
            var st = (from s in db.Foods
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st);
        }

        [EmployeePermission]
        [HttpPost]
        public ActionResult EditFood(Food f)
        {
            var db = new ZeroHungerEntities();
            var exst = (from s in db.Foods
                        where s.id == f.id
                        select s).SingleOrDefault();
            exst.status = f.status;

            db.SaveChanges();

            return RedirectToAction("HomeEmployee","Home");
        }

        [EmployeePermission]
        public ActionResult DetailsFood(int id)
        {
            var db = new ZeroHungerEntities();
            var st = (from s in db.Foods
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st);
        }
        [AdminPermission]
        public ActionResult DeleteFood(int id)
        {
            var db = new ZeroHungerEntities();
            var exst = db.Foods.Find(id);
            db.Foods.Remove(exst);
            db.SaveChanges();

            return RedirectToAction("HomeEmployee","Home");
        }





      





    }
}