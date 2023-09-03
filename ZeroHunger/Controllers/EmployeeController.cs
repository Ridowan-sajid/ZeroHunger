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
    
    public class EmployeeController : Controller
    {


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

            return RedirectToAction("LoginEmployee","Employee");

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

                    return RedirectToAction("HomeEmployee", "Employee");
                }
                TempData["Msg"] = "Username Password Invalid";
            }
            return View(em);
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

            return RedirectToAction("HomeEmployee","Home");
        }


      
        


       [AdminPermission]
        public ActionResult EmployeeFoodList()
        {
            var db = new ZeroHungerEntities();
            var efli = db.EmployeeFoods.ToList();
            return View(efli);
        }


        [AdminPermission]
        public ActionResult DeleteEmployeeFood(int id)
        {
            var db = new ZeroHungerEntities();
            var exst = db.EmployeeFoods.Find(id);
            db.EmployeeFoods.Remove(exst);
            db.SaveChanges();

            return RedirectToAction("EmployeeFoodList","Employee");
        }
        [AdminPermission]
        public ActionResult AllEmployee()
        {
            var db = new ZeroHungerEntities();
            return View(db.Employees.ToList());
        }
        [AdminPermission]
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var st = (from s in db.Employees
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st);
        }

        [AdminPermission]
        [HttpPost]
        public ActionResult EditEmployee(Employee f)
        {
            var db = new ZeroHungerEntities();
            var exst = (from s in db.Employees
                        where s.id == f.id
                        select s).SingleOrDefault();
            exst.name = f.name;
            exst.phone = f.phone;
            exst.email = f.email;
            

            db.SaveChanges();

            return RedirectToAction("AllEmployee", "Employee");
        }
        [AdminPermission]
        public ActionResult DeleteEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var exst = db.Employees.Find(id);
            
            db.Employees.Remove(exst);
            db.SaveChanges();

            return RedirectToAction("AllEmployee", "Employee");
        }


    }
}