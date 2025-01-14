using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class PhanQuyenNguoiDung : CoreEntity
    {

        [ForeignKey("NguoiDung")]
        public int UserId { get; set; }


        public virtual NguoiDung? NguoiDung { get; set; }
        [ForeignKey("QuyenSuDung")]

        public int QuyenId { get; set; } // Khóa ngoại của QuyenSuDung
        public virtual QuyenSuDung QuyenSuDung { get; set; }
    }
}
