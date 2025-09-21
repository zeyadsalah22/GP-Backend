using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Common
{
	public class BulkDeleteRequestDto
	{
		[Required]
		[MinLength(1)]
		public List<int> Ids { get; set; } = new List<int>();
	}
}


