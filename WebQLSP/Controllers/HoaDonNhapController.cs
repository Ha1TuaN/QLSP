using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLSP.Models;
using PagedList;
using System.IO;

namespace WebQLSP.Controllers
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
    }
}