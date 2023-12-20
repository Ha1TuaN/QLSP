using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;

namespace WebQLSP.Areas.Admin.Controllers
{
    [Authentication]
    public class DanhMucSanPhamController : Controller
    {

        // GET: DanhMucSanPham
        private QLSPEntities db = new QLSPEntities();
        public ActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var list = db.ProductCatalogs.ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCatalog model)
        {
            if (ModelState.IsValid)
            {
                db.ProductCatalogs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatalog list = db.ProductCatalogs.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCatalog model)
        {
            var b = db.ProductCatalogs.SingleOrDefault(x => x.ID == model.ID);
            if (ModelState.IsValid)
            {
                b.Name = model.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}