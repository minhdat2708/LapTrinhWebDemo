using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoWebBanVali.Models
{
    [MetadataTypeAttribute(typeof(tDanhMucSPMetadata))]
    public partial class tDanhMucSP
    {
        internal sealed class tDanhMucSPMetadata
        {
            [Display(Name = "Mã sản phẩm")]
            [Required(ErrorMessage = "Vui lòng nhập giá trị cho trường này!")]
            public string MaSP { get; set; }
            [Display(Name = "Tên sản phẩm")]
            public string TenSP { get; set; }
            [Display(Name = "Ngăn laptop")]
            public string NganLapTop { get; set; }
            [Display(Name = "Model")]
            public string Model { get; set; }
            [Display(Name = "Màu sắc")]
            public string MauSac { get; set; }
            [Display(Name = "Mã kích thước")]
            public string MaKichThuoc { get; set; }
            [Display(Name = "Cân nặng")]
            public Nullable<double> CanNang { get; set; }
            [Display(Name = "Độ nới")]
            public Nullable<double> DoNoi { get; set; }
            [Display(Name = "Thời gian bảo hành")]
            public Nullable<double> ThoiGianBaoHanh { get; set; }
            [Display(Name = "Giới thiệu")]
            public string GioiThieuSP { get; set; }
            [Display(Name = "Giá")]
            public Nullable<double> Gia { get; set; }
            [Display(Name = "Chiết khấu")]
            public Nullable<double> ChietKhau { get; set; }
        }

    }
}