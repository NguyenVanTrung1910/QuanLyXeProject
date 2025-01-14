using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Querys.Base
{
    public class CoreEntity
	{
		/// <summary>
		/// ID cho bản ghi
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Thời gian tạo cho bản ghi
		/// </summary>
		/// 
		[Required]
		public DateTime Created { get; private set; }

		/// <summary>
		/// Người tạo đầu tiên
		/// </summary>
		/// 

		public int CreatedBy { get; private set; }
		[NotMapped]
		public string CreatedByText { get; set; }

		/// <summary>
		/// Người sửa cuối cùng
		/// </summary>
		public int LastModifiedBy { get; private set; }
		[NotMapped]
		public string LastModifiedByText { get; set; }

		/// <summary>
		/// Thời gian chỉnh sửa cuối cho bản ghi
		/// </summary>
		/// 
		[Required]
		public DateTime LastModified { get; private set; }

		/// <summary>
		/// trạng thái duyệt của bản ghi
		/// </summary>

		[Required]
		public ModerationStatus ModerationStatus { get; set; }


        /// <summary>
        /// Phần chú thích của trạng thái duyệt
        /// </summary>
        [NotMapped]
		public string ModerationStatusTxt
		{
			get
			{
				switch (ModerationStatus)
				{
					case ModerationStatus.Approved:
						return "Duyệt";
					case ModerationStatus.Pending:
						return "Chờ duyệt";
					case ModerationStatus.Rejected:
						return "Từ chối";
					default:
						return "";
				}
			}
			set { }
		}
		[NotMapped]
		public int CurrentUserId { get; set; }

		public CoreEntity()
		{
			LastModifiedByText = string.Empty;
			CreatedByText = string.Empty;
		}


		public void FillDataForInsert(int IdUser)
		{
			this.Created = DateTime.Now;
			this.LastModified = DateTime.Now;
			this.CreatedBy = IdUser;
			this.LastModifiedBy = IdUser;
		}
		public void FillDataForUpdate(int IdUser)
		{
			this.LastModified = DateTime.Now;
			this.LastModifiedBy = IdUser;
		}
        public void FillDataForInsertHoiCo(int IdUser,DateTime ngayHoiCo)
        {
            this.Created = ngayHoiCo;
            this.LastModified = ngayHoiCo;
            this.CreatedBy = IdUser;
            this.LastModifiedBy = IdUser;
        }
    }




}
