using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map from Employee entity to EmployeeDTO for GET requests.
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")) // Combine first and last name
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.DateHired)) // Map hire date
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeType)) // Map employee type to role
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive)) // Map active status
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Map email

            // Map from CreateEmployeeDTO to Employee entity for POST/Create requests.
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today)) // Set hire date to today
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)) // Default new employees to active
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Map email

            // Map from UpdateEmployeeDTO to Employee entity for PUT/Update requests.
            CreateMap<UpdateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.Ignore()) // Ignore DateHired to prevent overwriting
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Map email
        }
    }
}
