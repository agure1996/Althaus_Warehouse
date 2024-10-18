using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Mappings
{
    /// <summary>
    /// The profile of the Employee entity, mapping functions to their corresponding DTOs and Entities.
    /// </summary>
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Constructor to define the mappings between Employee Entity and DTOs.
        /// </summary>
        public EmployeeProfile()
        {
            // Map from Employee entity to EmployeeDTO for GET requests.
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")) // Combine first and last names
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.DateHired))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeType));

            // Map from CreateEmployeeDTO to Employee entity for POST/Create requests.
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today));

            // Map from UpdateEmployeeDTO to Employee entity for PUT/Update requests.
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
