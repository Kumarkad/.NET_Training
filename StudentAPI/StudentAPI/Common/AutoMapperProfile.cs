using AutoMapper;
using StudentAPI.DTO;
using StudentAPI.Entity;

namespace StudentAPI.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Student,StudentModel>().ReverseMap();
        
        }
    }
}
