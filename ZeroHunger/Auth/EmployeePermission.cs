using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EM;

namespace ZeroHunger.Auth
{
    public class EmployeePermission : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (Employee)httpContext.Session["user"];
            if (user != null) return true;
            return false;
        }
    }
}