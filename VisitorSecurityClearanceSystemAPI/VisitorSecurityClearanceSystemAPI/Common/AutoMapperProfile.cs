using AutoMapper;
using VisitorSecurityClearanceSystemAPI.DTO;
using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Manager, ManagerModel>().ReverseMap();
            CreateMap<OfficeUser, OfficeUserModel>().ReverseMap();
            CreateMap<Visitor, VisitorModel>().ReverseMap();
            CreateMap<SecurityUser, SecurityUserModel>().ReverseMap();
        }
    }
}
