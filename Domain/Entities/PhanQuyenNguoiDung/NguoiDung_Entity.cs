
using Domain.Models;

namespace Domain.Entities
{
    public  class NguoiDung_Entity : NguoiDung
    {
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGiaSuaCuoi { get; set; }
        public int ID_NguoiTao { get; set; }
        public int ID_NguoiSua { get; set; }
        public List<VaiTro> VaiTroDuocPhan { get; set; } = new List<VaiTro> { };
        public string TinhTrangDocThantxt
        {
            get
            {
                switch (this.TinhTrangDocThan)
                {
                    case 1:
                        return "Đang trong mối quan hệ";
                    case 2:
                        return "Đã bị xích";
                    default:
                        return "Đang ế";

                }
            }
        }
        public string TokenAcessMedia { get; set; } = "chu-khoi-tao";
        public bool isQuanTriHT
        {
            get
            {
                //if (VaiTroDuocPhan.Any(x => x.MoTa.Equals("QTHT"))) return true;
                //else return false;
                 return false;

            }
        }
    }
}
