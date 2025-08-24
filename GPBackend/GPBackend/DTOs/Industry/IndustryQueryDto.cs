using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Industry
{
	public class IndustryQueryDto
	{
		public string? SearchTerm { get; set; }

		[Range(1, int.MaxValue)]
		public int PageNumber { get; set; } = 1;

		[Range(1, 100)]
		public int PageSize { get; set; } = 20;

		public string? SortBy { get; set; }
		public bool SortDescending { get; set; } = false;
	}
}


