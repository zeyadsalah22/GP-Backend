using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models;

namespace GPBackend.Models
{
	[ModelMetadataType(typeof(IndustryMetaData))]
	public partial class Industry
	{
	}

	public class IndustryMetaData
	{
		[Key]
		public int IndustryId { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; } = null!;

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public byte[]? Rowversion { get; set; }

		public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
	}
}


