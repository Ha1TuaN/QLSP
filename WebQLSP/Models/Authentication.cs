using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace WebQLSP.Models
{
    public class Authentication : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null )
            {
                // Nếu không, chuyển hướng đến trang đăng nhập
                filterContext.Result = new RedirectResult("~/TaiKhoan/Login");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}