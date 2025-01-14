using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class MenuQuanTri : CoreEntity
    {
        [Required]
        [MaxLength(256)]
        public string? TenMenu { get; set; }


        [MaxLength(50)]
        public string? Icon { get; set; }
        [MaxLength(256)]
        public string? LienKet { get; set; }

        public int? MenuChaId { get; set; }

        public int? ViTri { get; set; }

        public bool Category { get; set; }
        public ICollection<MenuNguoiDung> MenuNguoiDung { get; set; }
    }


	public class TreeMenuQuanTri : MenuQuanTri
	{
		public List<TreeMenuQuanTri>? children { get; set; }
	}


}
