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
            CreateMap<Officer, OfficerModel>().ReverseMap();
            CreateMap<Visitor,VisitorModel>().ReverseMap();
            CreateMap<Security, SecurityModel>().ReverseMap();
        }
    }
}
