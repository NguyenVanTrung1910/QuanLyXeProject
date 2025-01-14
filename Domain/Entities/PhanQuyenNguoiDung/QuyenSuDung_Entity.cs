using Domain.Models;
using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class QuyenSuDung_Entity : QuyenSuDung
    {
        //Bổ sung các prop mà không muốn xuất hiện ở DB trong đây
        public string TenNguoiDung { get; set; }

        public QuyenSuDung QuyenCha { get; set; }
        public NguoiDung MenuNguoiDung { get; set; }
        public VaiTro MenuVaiTro { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGiaSuaCuoi { get; set; }
        public int ID_NguoiTao { get; set; }
        public int ID_NguoiSua { get; set; }
    }
    public class PhanQuyenRequest
    {
        public int IdVaiTro { get; set; }
        public List<int> ListIdMenu { get; set; }
        public List<int> ListIdQuyen { get; set; }
        public int NguoiDungId { get; set; }

    }
}
