using AutoMapper;
using GPBackend.DTOs.Industry;
using GPBackend.Models;

namespace GPBackend.Profiles
{
	public class IndustryProfile : Profile
	{
		public IndustryProfile()
		{
			CreateMap<Industry, IndustryResponseDto>();
		}
	}
}


