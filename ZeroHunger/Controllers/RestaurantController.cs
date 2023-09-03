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
    
    public class RestaurantController : Controller
    {
        
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

            return RedirectToAction("LoginRestaurant","Restaurant");

        }

    }
}