using System;
using GPBackend.Models.Enums;

namespace GPBackend.Models
{
	public class ApplicationStageHistory
	{
		public int Id { get; set; }
		public int ApplicationId { get; set; }
		public ApplicationStage Stage { get; set; }
		public DateOnly ReachedDate { get; set; }
		public string? Note { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }
		public byte[] Rowversion { get; set; } = null!;

		public virtual Application Application { get; set; } = null!;
	}
}


