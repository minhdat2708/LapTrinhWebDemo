using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoWebBanVali.Models
{
    [MetadataTypeAttribute(typeof(tKhachHangMetadata))]

    public partial class tKhachHang
	{
		internal sealed class tKhachHangMetadata
        {
            [Display(Name = "Mã khách hàng")]
            [Required(ErrorMessage = "Vui lòng nhập giá trị cho trường này!")]
            public int MaKH { get; set; }
            [Display(Name = "Tên người dùng")]
            public string TenNguoiDung { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "Gồm ít nhất 8 ký tự, gồm chữ số, chữ cái, ký tự đặc biệt")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")]
            public string MatKhau { get; set; }
            [Display(Name = "Tên khách hàng")]
            [Required(ErrorMessage = "Nhập tên khách hàng")]
            public string TenKH { get; set; }
            [Display(Name = "Địa chỉ")]
            public string DiaChi { get; set; }
            [Display(Name = "Điện thoại")]
            public string DienThoai { get; set; }
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Display(Name = "Giới tính")]
            public Nullable<bool> GioiTinh { get; set; }
        }
	}
}