using DemoWebBanVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWebBanVali.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        WebBanVaLiEntities db = new WebBanVaLiEntities();

        #region Giỏ hàng
        // Lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>; 
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        // Thêm vào giỏ hàng
        public ActionResult ThemGioHang(string sMaSP, string strUrl)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.Single(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lấy ra session giỏ hàng 
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spGioHang = lstGioHang.Find(n=>n.sMaSP == sMaSP);
            if (spGioHang == null)
            {
                spGioHang = new GioHang(sMaSP);
                lstGioHang.Add(spGioHang);
                return Redirect(strUrl);
            }
            else
            {
                spGioHang.iSoLuong++;
                return Redirect(strUrl);
            }
        }

        // Cập nhật giỏ hàng
        public ActionResult CapNhatSoLuong(string MaSP, FormCollection form)
        {
            //kiểm tra mã sản phẩm
            tDanhMucSP sanPham = db.tDanhMucSPs.Single(n => n.MaSP == MaSP);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang gioHang = lstGioHang.Single(n => n.sMaSP == MaSP);
            if (gioHang != null)
            {
                gioHang.iSoLuong = int.Parse(form.Get("txtSoLuong").ToString());
            }
            return RedirectToAction("CapNhatGioHang");
        }

        public ActionResult XoaGioHang(string sMaSP, FormCollection f)
        {
            //kiểm tra mã sản phẩm
            tDanhMucSP sanpham = db.tDanhMucSPs.Single(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(n => n.sMaSP == sMaSP);
            if (sp != null)
            { 
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP); 
            }
            if (lstGioHang.Count == 0)
            {
                RedirectToAction("Index", "SanPham"); 
            }
            return RedirectToAction("CapNhatGioHang");
        }

        public ActionResult CapNhatGioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPham");
            }
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            ViewBag.TongTien = TongTien();
            ViewBag.TongSoLuong = TongSoLuong();
            return View(lstGioHang);
        }

        //Xây dựng trang Giỏ Hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPham");
            }
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            ViewBag.TongTien = TongTien();
            ViewBag.TongSoLuong = TongSoLuong();
            return View(lstGioHang);        
        }

        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return tongSoLuong;
        }

        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);

            }
            return dTongTien;
        }

        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }
        #endregion

        #region Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            return View();
        }

        #endregion

    }
}