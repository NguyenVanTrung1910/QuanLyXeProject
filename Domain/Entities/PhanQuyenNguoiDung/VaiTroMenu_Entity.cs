using Domain.Models;
using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VaiTroMenu_Entity : VaiTroMenu
    {
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGiaSuaCuoi { get; set; }
        public int ID_NguoiTao { get; set; }
        public int ID_NguoiSua { get; set; }
    }
}
