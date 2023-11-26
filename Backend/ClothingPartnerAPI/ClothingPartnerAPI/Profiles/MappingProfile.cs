using AutoMapper;
using ClothingPartnerAPI.DTO;
using ClothingPartnerAPI.Models;

namespace ClothingPartnerAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<DesignationDTO, Designation>();
            CreateMap<TeamDTO, Team>();
        }
    }
}
