using System;

namespace GPBackend.Models
{
	public class UserCompanyTag
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CompanyId { get; set; }
		public string Tag { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public virtual UserCompany UserCompany { get; set; } = null!;
	}
}


