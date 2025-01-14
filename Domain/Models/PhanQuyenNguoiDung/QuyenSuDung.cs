using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class QuyenSuDung : CoreEntity
    {
        [Required]
        public string? MaQuyen { get; set; }

        [Required]
        public string? TenQuyen { get; set; }
        public string? MoTa { get; set; }
        public int? QuyenSuDungCha { get; set; }
        public ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDung { get; set; }
    }
    public class TreeQuyenSuDung : QuyenSuDung
    {
        public List<TreeQuyenSuDung>? children { get; set; }
    }
}
