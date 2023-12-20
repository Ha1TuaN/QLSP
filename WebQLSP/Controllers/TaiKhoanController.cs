using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;

namespace WebQLSP.Controllers
{
    [AllowAnonymous]
    public class TaiKhoanController : Controller
    {
        QLSPEntities db = new QLSPEntities();
        public ActionResult Login()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string username = userlog["username"].ToString();
            string password = userlog["pass"].ToString();
            var islogin = db.Accounts.SingleOrDefault(x => x.username.Equals(username) && x.pass.Equals(password));
            if (ModelState.IsValid)
            {
                if (islogin != null)
                {
                    if (username == "admin")
                    {
                        Session["user"] = islogin;
                        return RedirectToAction("Index", "Admin/Home");
                    }
                    else
                    {
                        Session["user"] = islogin;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                    return View();
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Register model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Emp_Name = model.Name,
                    Emp_Phone = model.PhoneNumber,
                };
                db.Employees.Add(employee);
                db.SaveChanges();
                var account = new Account
                {
                    username = model.UserName,
                    pass = model.Password,
                    Emp_ID = employee.Emp_ID,
                };
                db.Accounts.Add(account);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePass model)
        {
            var user = (Account)Session["user"];
            var acc = db.Accounts.SingleOrDefault(x => x.username == user.username);
            if (ModelState.IsValid && acc != null)
            {
                if (model.OldPassword == acc.pass)
                {
                    acc.pass = model.NewPassword;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                }
            }

            return View(model);
        }
    }
}
        