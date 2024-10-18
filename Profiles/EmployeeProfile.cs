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
            /// <summary>
            /// Map from Employee entity to GetEmployeeDTO for GET requests.
            /// </summary>
            CreateMap<Employee, GetEmployeeDTO>();

            /// <summary>
            /// Map from CreateEmployeeDTO to Employee entity for POST/Create requests.
            /// Automatically sets DateHired to today's date for simplicity when inputting into Database.
            /// </summary>
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today)); 

            /// <summary>
            /// Map from UpdateEmployeeDTO to Employee entity for PUT/Update requests.
            /// </summary>
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
