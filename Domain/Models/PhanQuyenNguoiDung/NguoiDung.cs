using System.ComponentModel.DataAnnotations;
using Domain.Querys.Base;

namespace Domain.Models
{
    public class NguoiDung : CoreEntity
    {

        [Required]
        public string? TenNguoiDung { get; set; }

        [Required]
        public string? TenDangNhap { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? MatKhau { get; set; }

        public string? DienThoai { get; set; }
        public string? Idp { get; set; }
        public DateTime? NgaySinh { get; set; }

        [MaxLength(256)]
        public string? DiaChi { get; set; }

        [MaxLength(256)]
        public string? AnhDaiDien { get; set; }

        public string? TokenJira { get; set; }

        /// <summary>
        /// 0 độc thân
        /// 1 đã có ny
        /// 2 đã có vợ/chồng
        /// </summary>
        public int TinhTrangDocThan { get; set; }






        public ICollection<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }
        public ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDung { get; set; }
        public ICollection<MenuNguoiDung> MenuNguoiDung { get; set; }
    }

}
