using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;

namespace WebQLSP.Areas.Admin.Controllers
{
    [Authentication]
    public class NhaCCController : Controller
    {
        private QLSPEntities db = new QLSPEntities();

        // GET: Suppliers
        public ActionResult Index(string currentFilter, int? page, string SearchString = "")
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                var list = db.Suppliers.Where(x => x.Sup_ID.ToUpper().Contains(SearchString.ToUpper())).ToList();
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var list = db.Suppliers.ToList();
                return View(list.ToPagedList(pageNumber, pageSize));
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sup_ID,Sup_Name,Sup_Address,Sup_Phone,isDelete")] Supplier supplier)
        {
            var sup = db.Suppliers.SingleOrDefault(x => x.Sup_ID == supplier.Sup_ID);
            if (sup == null)
            {
                if (ModelState.IsValid)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(supplier);
            }
            else
            {
                ModelState.AddModelError("", "Nhà cung cấp đã tồn tại trong bảng");
                return View();
            }
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sup_ID,Sup_Name,Sup_Address,Sup_Phone,isDelete")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        public string Delete(string id)
        {
            try
            {
                var emp = db.Suppliers.SingleOrDefault(x => x.Sup_ID == id);
                if (emp != null)
                {
                    emp.isDelete = true;
                    db.SaveChanges();
                    return "Xóa nhà cung cấp thành công";
                }
                else
                {
                    return "Không tìm thấy nhà cung cấp có mã số " + id;
                }
            }
            catch (Exception ex)
            {
                return "Xóa nhà cung cấp thất bại" + ex.Message;
            }
        }
    }
}