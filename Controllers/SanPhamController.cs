using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DemoWebBanVali.Models;
using PagedList;
using PagedList.Mvc;

namespace DemoWebBanVali.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        WebBanVaLiEntities db = new WebBanVaLiEntities();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult LoaiPartial()
        {
            return PartialView(db.tLoaiSPs.ToList());
        }

        public ViewResult SanPhamTheoLoai(int? page, string MaLoai = "vali")
        {
            int pageSize = 12; //Số sản phẩm trong một trang
            int pagenum = (page ?? 1);

            tLoaiSP loaiSanPham = db.tLoaiSPs.SingleOrDefault(n => n.MaLoai == MaLoai); 
            if (loaiSanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Khai báo một sản phẩm trong bảng danh mục sản phẩm
            List<tDanhMucSP> lstSanPham = db.tDanhMucSPs.Where(n => n.MaLoai == MaLoai).OrderBy(n=>n.TenSP).ToList();
            if (lstSanPham.Count == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.tDanhMucSPs.ToList();
            ViewBag.MaLoai = MaLoai;
            return View(lstSanPham.ToPagedList(pagenum, pageSize));
        }

        public ViewResult ChiTietSanPham(string MaSP = "bacackeroirbl")
        {
            tDanhMucSP sanPham = db.tDanhMucSPs.SingleOrDefault(n=>n.MaSP == MaSP);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }

        [HttpGet]
        public ActionResult ThemSanPham()
        {
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux.ToList().OrderBy(n=>n.ChatLieu), "MaChatLieu", "ChatLieu");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs.ToList().OrderBy(n=>n.KichThuoc), "MaKichThuoc", "KichThuoc");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes.ToList().OrderBy(n=>n.HangSX), "MaHangSX", "HangSX");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias.ToList().OrderBy(n=>n.TenNuoc), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs.ToList().OrderBy(n=>n.Loai), "MaLoai", "Loai");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs.ToList().OrderBy(n=>n.TenLoai), "MaDT", "TenLoai");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham([Bind(Include = "MaSP, TenSP, MaChatLieu, NganLapTop, Model, MauSac, MaKichThuoc, CanNang, DoNoi, MaHangSX, MaNuocSX, MaDacTinh, Website, ThoiGianBaoHanh,GioiThieuSP, Gia, ChietKhau, MaLoai, MaDT, Anh")] tDanhMucSP sanPham)
        {
            if (ModelState.IsValid)
            {
                db.tDanhMucSPs.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        [HttpGet]
        public ActionResult SuaSanPham(String MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            tDanhMucSP sanPham = db.tDanhMucSPs.Find(MaSP);
            if (sanPham == null)
            {
                return HttpNotFound(); 
            }
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux.ToList().OrderBy(n => n.ChatLieu), "MaChatLieu", "ChatLieu");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs.ToList().OrderBy(n => n.KichThuoc), "MaKichThuoc", "KichThuoc");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes.ToList().OrderBy(n => n.HangSX), "MaHangSX", "HangSX");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias.ToList().OrderBy(n => n.TenNuoc), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs.ToList().OrderBy(n => n.Loai), "MaLoai", "Loai");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs.ToList().OrderBy(n => n.TenLoai), "MaDT", "TenLoai");
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham([Bind(Include = "MaSP, TenSP, MaChatLieu, NganLapTop, Model, MauSac, MaKichThuoc, CanNang, DoNoi, MaHangSX, MaNuocSX, MaDacTinh, Website, ThoiGianBaoHanh,GioiThieuSP, Gia, ChietKhau, MaLoai, MaDT, Anh")] tDanhMucSP sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaSanPham(String MaSP)
        {
            tDanhMucSP sanPham = db.tDanhMucSPs.Single(n => n.MaSP == MaSP);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }

        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa (string MaSP)
        {
            tDanhMucSP sanPham = db.tDanhMucSPs.Single(n => n.MaSP == MaSP);
            var anhSP = from p in db.tAnhSPs where p.MaSP == sanPham.MaSP select p;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tAnhSPs.RemoveRange(anhSP);
            db.tDanhMucSPs.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}