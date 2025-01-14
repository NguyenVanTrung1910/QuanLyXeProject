using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class VaiTroNguoiDung : CoreEntity
    {
        [ForeignKey("NguoiDung")]
        public int UserId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int VaiTroId { get; set; }
        public VaiTro VaiTro { get; set; }

        
    }


}
