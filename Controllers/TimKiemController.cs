using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWebBanVali.Models;
using PagedList;

namespace DemoWebBanVali.Controllers
{
    public class TimKiemController : Controller
    {
        WebBanVaLiEntities db = new WebBanVaLiEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult SearchResults(FormCollection form, int? page)
        {
            string search_key = form.Get("txtSearch").ToString();
            List<tDanhMucSP> lstSearchResult  = db.tDanhMucSPs.Where(n => n.TenSP.Contains(search_key)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstSearchResult.Count == 0)
            {
                ViewBag.Message = "Không tìm thấy sản phẩm bạn tìm kiếm";
                return View(db.tDanhMucSPs.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                //Nếu không tìm thấy sản phẩm nào thì xuất ra toàn bộ sản phẩm
            }
            ViewBag.KeyWord = search_key;
            ViewBag.Message = "Tìm thấy " + lstSearchResult.Count + " sản phẩm";
            return View(lstSearchResult.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult SearchResults(int? page, string searchKey)
        {
            ViewBag.KeyWord = searchKey;
            List<tDanhMucSP> lstSearchResult = db.tDanhMucSPs.Where(n => n.TenSP.Contains(searchKey)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstSearchResult.Count == 0)
            {
                ViewBag.Message = "Không tìm thấy sản phẩm bạn tìm kiếm";
                return View(db.tDanhMucSPs.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                //Nếu không tìm thấy sản phẩm nào thì xuất ra toàn bộ sản phẩm
            }
            ViewBag.Message = "Tìm thấy " + lstSearchResult.Count + " sản phẩm";
            return View(lstSearchResult.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }

    }
}