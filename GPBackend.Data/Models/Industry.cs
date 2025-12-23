using System;
using System.Collections.Generic;

namespace GPBackend.Models
{
	public partial class Industry
	{
		public int IndustryId { get; set; }

		public string Name { get; set; } = null!;

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public byte[]? Rowversion { get; set; } = null!;

		public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
	}
}


