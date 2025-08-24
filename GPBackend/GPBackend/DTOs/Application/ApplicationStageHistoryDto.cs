using System;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Application
{
	public class ApplicationStageHistoryDto
	{
		public ApplicationStage Stage { get; set; }
		public DateOnly Date { get; set; }
		public string? Note { get; set; }
	}
}


