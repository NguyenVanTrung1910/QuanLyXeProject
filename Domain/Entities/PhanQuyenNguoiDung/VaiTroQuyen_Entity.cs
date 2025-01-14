using Domain.Models;
using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VaiTroQuyen_Entity : VaiTroQuyen
    {
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGiaSuaCuoi { get; set; }
        public int ID_NguoiTao { get; set; }
        public int ID_NguoiSua { get; set; }
    }
    public class PhanVaiTroRequest
    {
        public int IdNguoiDung { get; set; }
        public List<int> ListIdVaiTro { get; set; }
        public List<int> ListIdMenu { get; set; }
        public List<int> ListIdQuyen { get; set; }
        public int CurrentUserId { get; set; }

    }
}
