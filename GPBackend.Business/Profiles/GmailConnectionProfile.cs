using AutoMapper;
using GPBackend.DTOs.Gmail;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class GmailConnectionProfile : Profile
    {
        public GmailConnectionProfile()
        {
            // Map from GmailConnection entity to response DTO
            CreateMap<GmailConnection, GmailConnectionResponseDto>()
                .ForMember(dest => dest.IsTokenExpired, opt => opt.MapFrom(src => src.IsTokenExpired));

            // Note: We don't map tokens to response DTO for security
            // ActiveGmailConnectionDto will be handled manually in service
            // to include decrypted tokens only for n8n
        }
    }
}

