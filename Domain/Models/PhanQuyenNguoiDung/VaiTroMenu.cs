using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{

    public class VaiTroMenu : CoreEntity
    {


        public int VaiTroId { get; set; }
        public VaiTro VaiTro { get; set; }

        public int MenuQuanTriId { get; set; }
        public MenuQuanTri MenuQuanTri { get; set; }
    }
}
