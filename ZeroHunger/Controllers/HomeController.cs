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
    
    public class HomeController : Controller
    {
        [AllowAnonymous]
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
        [RestaurantPermission]
        [HttpPost]
        public ActionResult CreateFood(Food f)
        {
            var db = new ZeroHungerEntities();
            f.status = "None";
            db.Foods.Add(f);
            db.SaveChanges();

            return RedirectToAction("Home");

        }
        [RestaurantPermission]
        public ActionResult Home()
        {
            
            var db = new ZeroHungerEntities();
            return View(db.Foods.ToList());
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginRestaurant()
        {

            return View();
        }
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateRestaurant()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateRestaurant(Restaurant res)
        {
            var db = new ZeroHungerEntities();
            db.Restaurants.Add(res);
            db.SaveChanges();

            return RedirectToAction("LoginRestaurant");

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateEmployee(Employee em)
        {
            var db = new ZeroHungerEntities();
            db.Employees.Add(em);
            db.SaveChanges();

            return RedirectToAction("LoginEmployee");

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginEmployee()
        {
            return View();
        }
        [AllowAnonymous]
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
                    Session["role"] = user.role;
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
        
        [EmployeePermission]
        public ActionResult HomeEmployee()
        {
            var db = new ZeroHungerEntities();
            return View(db.Foods.ToList());
        }

        [AdminPermission]
        [HttpGet]
        public ActionResult CreateEmployeeFood()
        {
            var db = new ZeroHungerEntities();
            var FoodLi = db.Foods.ToList();
            var EmployeeLi = db.Employees.ToList();
            ViewBag.st = EmployeeLi;
            return View(FoodLi);
        }
        [AdminPermission]
        [HttpPost]
        public ActionResult CreateEmployeeFood(EmployeeFood ef)
        {
            var db = new ZeroHungerEntities();
            db.EmployeeFoods.Add(ef);
            db.SaveChanges();


            var exst = (from s in db.Foods
                        where s.id == ef.FoodID
                        select s).SingleOrDefault();
            exst.status = "Processing";
            db.SaveChanges();

            return RedirectToAction("HomeEmployee");
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

            //db.Entry(exst).CurrentValues.SetValues(f);
            db.SaveChanges();

            return RedirectToAction("HomeEmployee");
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

            return RedirectToAction("HomeEmployee");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }


       [AdminPermission]
        public ActionResult EmployeeFoodList()
        {
            var db = new ZeroHungerEntities();
            var efli = db.EmployeeFoods.ToList();
            return View(efli);
        }


        /*[HttpGet]
        public ActionResult EditEmployeeFood(int id)
        {
            var db = new ZeroHungerEntities();
            var st =db.EmployeeFoods.Find(id);
            return View(st);
        }


        [HttpPost]
        public ActionResult EditEmployeeFood(EmployeeFood f)
        {
            var db = new ZeroHungerEntities();
            var exst = (from s in db.EmployeeFoods
                        where s.id == f.id
                        select s).SingleOrDefault();
            

            db.Entry(exst).CurrentValues.SetValues(f);
            db.SaveChanges();

            return RedirectToAction("HomeEmployee");
        }*/

        [AdminPermission]
        public ActionResult DeleteEmployeeFood(int id)
        {
            var db = new ZeroHungerEntities();
            var exst = db.EmployeeFoods.Find(id);
            db.EmployeeFoods.Remove(exst);
            db.SaveChanges();

            return RedirectToAction("EmployeeFoodList");
        }






    }
}