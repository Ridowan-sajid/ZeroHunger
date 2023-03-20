using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EM;

namespace ZeroHunger.Auth
{
    public class RestaurantPermission:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (Restaurant)httpContext.Session["user"];
            if (user != null) return true;
            return false;
        }
    }
}