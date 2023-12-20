using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;

namespace WebQLSP.Areas.Admin.Controllers
{
    [Authentication]
    public class NhanVienController : Controller
    {
        private QLSPEntities db = new QLSPEntities();
        public ActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var list = db.Employees.Where(x => x.isDelete == false).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Employee model)
        {
            db.Employees.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var list = db.Employees.SingleOrDefault(emp => emp.Emp_ID == id);
            return View(list);
        }
        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            var emp = db.Employees.SingleOrDefault(e => e.Emp_ID == model.Emp_ID);
            if (emp != null)
            {
                emp.Emp_Name = model.Emp_Name;
                emp.Emp_Phone = model.Emp_Phone;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public string Delete(int id)
        {
            try
            {
                var emp = db.Employees.SingleOrDefault(x => x.Emp_ID == id);
                if (emp != null)
                {
                    emp.isDelete = true;
                    db.SaveChanges();
                    return "Xóa nhân viên thành công";
                }
                else
                {
                    return "Không tìm thấy nhân viên có mã số " + id;
                }
            }
            catch (Exception ex)
            {
                return "Xóa nhan viên thất bại" + ex.Message;
            }
        }
    }
}