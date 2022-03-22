using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWebBanVali.Models
{
    public class GioHang
    {
        WebBanVaLiEntities db= new WebBanVaLiEntities();

        public string sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get; set; }

        public GioHang(string MaSP)
        {
            sMaSP = MaSP;
            tDanhMucSP sanPham = db.tDanhMucSPs.Single(n=>n.MaSP == sMaSP);
            sTenSP = sanPham.TenSP;
            sHinhAnh = sanPham.Anh;
            dDonGia = double.Parse(sanPham.Gia.ToString());
            iSoLuong = 1;
            dThanhTien = dDonGia * iSoLuong;
        }

    }
}