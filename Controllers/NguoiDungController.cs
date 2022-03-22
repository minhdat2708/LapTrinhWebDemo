using DemoWebBanVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWebBanVali.Controllers
{
    public class NguoiDungController : Controller
    {
        WebBanVaLiEntities db = new WebBanVaLiEntities(); 

        [HttpGet]
        // GET: NguoiDung
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(tKhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.tKhachHangs.Add(kh);
                db.SaveChanges();
            }
            return View(kh);
        }

        [HttpGet]
        public ActionResult DangNhap() {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection form)
        {
            string taiKhoan = form.Get("txtUsername").ToString();
            string matKhau = form.Get("txtPassword").ToString();

            tKhachHang kh = db.tKhachHangs.SingleOrDefault(n=>n.TenNguoiDung == taiKhoan && n.MatKhau == matKhau);

            if (kh != null )
            {
                ViewBag.Message = "Chúc mừng đăng nhập thành công";
                return View();
            }
            ViewBag.Message = "Sai tên người dùng hoặc mật khẩu";
            return View();
        }
    }
}