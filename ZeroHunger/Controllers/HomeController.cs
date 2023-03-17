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
        public ActionResult Home()
        {
            return View();  
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
/*                        Session["user"] = user;
                        var returnUrl = Request["ReturnUrl"];
                         if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }*/
                        return RedirectToAction("Index", "Home");
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

    }
}