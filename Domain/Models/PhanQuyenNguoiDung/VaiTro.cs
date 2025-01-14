using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class VaiTro : CoreEntity
    {
        [Required]
        public string TenVaiTro { get; set; }

        public string MoTa { get; set; }

        public ICollection<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }

    }
}
