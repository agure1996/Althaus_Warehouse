using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map from Employee entity to GetEmployeeDTO (for GET requests)
            CreateMap<Employee, GetEmployeeDTO>();

            // Map from CreateEmployeeDTO to Employee entity (for POST/Create requests) and set datehired automatically on create
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today));  // Set DateHired automatically on create

            // Map from UpdateEmployeeDTO to Employee entity (for PUT/Update requests)
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
