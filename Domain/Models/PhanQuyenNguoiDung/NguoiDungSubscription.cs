using Domain.Querys.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class NguoiDungSubscription : CoreEntity
    {
        [ForeignKey("NguoiDung")]
        public int UserId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        [MaxLength(1000)]
        public string? SubscriptionData { get; set; }
        [MaxLength(300)]
        public string keys { get; set; } = string.Empty;


    }


}
