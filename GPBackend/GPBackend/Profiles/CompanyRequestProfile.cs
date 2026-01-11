using AutoMapper;
using GPBackend.DTOs.CompanyRequest;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class CompanyRequestProfile : Profile
    {
        public CompanyRequestProfile()
        {
            // DTO to Domain
            CreateMap<CompanyRequestCreateDto, CompanyRequest>()
                .ForMember(dest => dest.RequestId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.RequestStatus, opt => opt.Ignore())
                .ForMember(dest => dest.RequestedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewedByAdminId, opt => opt.Ignore())
                .ForMember(dest => dest.RejectionReason, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewedByAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.Industry, opt => opt.Ignore());
        }
    }
}

