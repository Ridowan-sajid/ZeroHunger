using EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EM;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateFood()
        {
            var db = new ZeroHungerEntities();
            var id = Session["id"];
            var res = db.Restaurants.Find(id);
            return View(res);
        }
        [HttpPost]
        public ActionResult CreateFood(Food f)
        {
            var db = new ZeroHungerEntities();
            db.Foods.Add(f);
            db.SaveChanges();

            return RedirectToAction("Home");

        }

        public ActionResult Home()
        {
            var db = new ZeroHungerEntities();
            return View(db.Foods.ToList());
        }

        [HttpGet]
        public ActionResult LoginRestaurant()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LoginRestaurant(LoginModel res)
        {
                if (ModelState.IsValid)
                {
                ZeroHungerEntities db = new ZeroHungerEntities();
                    var user = (from u in db.Restaurants
                                where u.name.Equals(res.name)
                                && u.password.Equals(res.password)
                                select u).SingleOrDefault();
                    if (user != null)
                    {
                        Session["user"] = user;
                        Session["id"] = user.id;
                    /*  var returnUrl = Request["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }*/
                        return RedirectToAction("Home", "Home");
                    }
                    TempData["Msg"] = "Username Password Invalid";
                }
                return View(res);
        }

        [HttpGet]
        public ActionResult CreateRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRestaurant(Restaurant res)
        {
            var db = new ZeroHungerEntities();
            db.Restaurants.Add(res);
            db.SaveChanges();

            return RedirectToAction("LoginRestaurant");

        }




        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee em)
        {
            var db = new ZeroHungerEntities();
            db.Employees.Add(em);
            db.SaveChanges();

            return RedirectToAction("LoginEmployee");

        }


        [HttpGet]
        public ActionResult LoginEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginEmployee(LoginModel em)
        {
            if (ModelState.IsValid)
            {
                ZeroHungerEntities db = new ZeroHungerEntities();
                var user = (from u in db.Employees
                            where u.name.Equals(em.name)
                            && u.password.Equals(em.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    Session["id"] = user.id;
                    /*  var returnUrl = Request["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }*/
                    return RedirectToAction("HomeEmployee", "Home");
                }
                TempData["Msg"] = "Username Password Invalid";
            }
            return View(em);
        }

        public ActionResult HomeEmployee()
        {
            var db = new ZeroHungerEntities();
            return View(db.Foods.ToList());
        }

        [HttpGet]
        public ActionResult CreateEmployeeFood()
        {
            var db = new ZeroHungerEntities();
            var FoodLi = db.Foods.ToList();
            var EmployeeLi = db.Employees.ToList();
            ViewBag.st = EmployeeLi;
            return View(FoodLi);
        }
        [HttpPost]
        public ActionResult CreateEmployeeFood(EmployeeFood ef)
        {
            var db = new ZeroHungerEntities();
            db.EmployeeFoods.Add(ef);
            db.SaveChanges();

            return RedirectToAction("HomeEmployee");
        }



        [HttpGet]
        public ActionResult EditFood(int id)
        {
            var db = new ZeroHungerEntities();
            var st = (from s in db.Foods
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st);
        }
        [HttpPost]
        public ActionResult EditFood(Food f)
        {
            var db = new ZeroHungerEntities();
            var exst = (from s in db.Foods
                        where s.id == f.id
                        select s).SingleOrDefault();
            exst.status = f.status;

            //db.Entry(exst).CurrentValues.SetValues(f);
            db.SaveChanges();

            return RedirectToAction("HomeEmployee");
        }


        public ActionResult DetailsFood(int id)
        {
            var db = new ZeroHungerEntities();
            var st = (from s in db.Foods
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st);
        }

    }
}