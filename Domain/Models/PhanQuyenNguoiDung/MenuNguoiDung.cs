using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class MenuNguoiDung : CoreEntity
    {
        [ForeignKey("NguoiDung")]
        public int UserId { get; set; }


        public virtual NguoiDung? NguoiDung { get; set; }

        [ForeignKey("MenuQuanTri")]
        public int MenuId { get; set; } // Khóa ngoại của MenuQuanTri
        public virtual MenuQuanTri? MenuQuanTri { get; set; }
    }
}
