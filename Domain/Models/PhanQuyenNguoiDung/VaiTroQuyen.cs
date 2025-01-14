using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{

    public class VaiTroQuyen : CoreEntity
    {
        public int VaiTroId { get; set; }
        public VaiTro VaiTro { get; set; }

        public int QuyenSuDungId { get; set; }
        public QuyenSuDung QuyenSuDung { get; set; }
    }
}
