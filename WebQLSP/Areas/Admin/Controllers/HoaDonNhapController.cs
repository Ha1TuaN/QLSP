using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;
using PagedList;

namespace WebQLSP.Areas.Admin.Controllers
{
    [Authentication]
    public class HoaDonNhapController : Controller
    {
        // GET: HoaDonNhap
        private QLSPEntities db = new QLSPEntities();
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
                var list = db.InvoiceIns.Where(x => x.Inv_ID.ToUpper().Contains(SearchString.ToUpper()) && x.isDelete == false).ToList();
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var list = db.InvoiceIns.Where(x => x.isDelete == false).ToList();
                return View(list.ToPagedList(pageNumber, pageSize));
            }

        }


        public ActionResult Create()
        {
            InvoiceIn inv = new InvoiceIn();
            inv.DetailInvoiceIns.Add(new DetailInvoiceIn() { ID = 1 });
            return View(inv);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceIn invoice)
        {
            var inv = db.InvoiceIns.SingleOrDefault(x => x.Inv_ID == invoice.Inv_ID);
            if (inv == null)
            {
                if (ModelState.IsValid)
                {
                    db.InvoiceIns.Add(invoice);

                    foreach (var detail in invoice.DetailInvoiceIns)
                    {
                        Product prod = db.Products.SingleOrDefault(p => p.Prod_ID == detail.Prod_ID);
                        if (prod != null)
                        {
                            prod.Quantity += detail.Quantity;
                        }

                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại trong bảng");
                    return View();
                }
            }
            return View(invoice);
        }

        public ActionResult Edit(string id)
        {
            InvoiceIn inv = db.InvoiceIns.SingleOrDefault(i => i.Inv_ID == id);
            if (inv == null)
            {
                return HttpNotFound();
            }
            inv.DetailInvoiceIns = db.DetailInvoiceIns.Where(d => d.Inv_ID == id).ToList();
            return View(inv);
        }

        [HttpPost]
        public ActionResult Edit(InvoiceIn model)
        {
            InvoiceIn inv = db.InvoiceIns.SingleOrDefault(i => i.Inv_ID == model.Inv_ID);
            if (inv == null)
            {
                return HttpNotFound();
            }
            inv.Inv_DateIn = model.Inv_DateIn;

            for (int i = 0; i < inv.DetailInvoiceIns.Count; i++)
            {
                inv.DetailInvoiceIns[i].Prod_ID = model.DetailInvoiceIns[i].Prod_ID;
                inv.DetailInvoiceIns[i].Quantity = model.DetailInvoiceIns[i].Quantity;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            InvoiceIn inv = db.InvoiceIns.SingleOrDefault(x => x.Inv_ID == id);
            if (inv == null)
            {
                return HttpNotFound();
            }
            var detail = db.DetailInvoiceIns.Where(d => d.Inv_ID == id).ToList();
            inv.DetailInvoiceIns = detail;
            decimal total = 0;
            foreach (var item in detail)
            {
                total += item.Inv_Total;
            }

            ViewBag.Total = total.ToString("#,##0.00");
            return View(inv);
        }

        public string Delete(string id)
        {
            try
            {
                var invoice = db.InvoiceIns.SingleOrDefault(i => i.Inv_ID == id);
                if (invoice != null)
                {
                    invoice.isDelete = true;
                    db.SaveChanges();
                    return "Xóa hóa đơn thành công";
                }
                else
                {
                    return "Không tìm thấy hóa đơn có mã số " + id;
                }
            }
            catch (Exception ex)
            {
                return "Xóa hóa đơn thất bại" + ex.Message;
            }
        }
        public ActionResult AddProd()
        {
            return View();
        }

        
    }
}