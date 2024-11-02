using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Mappings
{
    /// <summary>
    /// AutoMapper profile for employee mappings.
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            /// <summary>
            /// Map from Employee entity to EmployeeDTO for GET requests.
            /// Combines first and last names into a single Name property, 
            /// and maps other relevant properties.
            /// </summary>
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.DateHired))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeType.ToString()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            /// <summary>
            /// Map from CreateEmployeeDTO to Employee entity for POST/Create requests.
            /// Sets the hire date to today, marks the employee as active,
            /// and hashes the password after mapping.
            /// </summary>
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => Enum.Parse<EmployeeType>(src.EmployeeType)))
                .AfterMap((src, dest) => dest.PasswordHash = HashPassword(src.Password));

            /// <summary>
            /// Map from UpdateEmployeeDTO to Employee entity for PUT/Update requests.
            /// Ignores the hire date and hashes the password if provided.
            /// </summary>
            CreateMap<UpdateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrEmpty(src.Password))
                    {
                        dest.PasswordHash = HashPassword(src.Password);
                    }
                });

            /// <summary>
            /// Hashes the password using BCrypt.
            /// </summary>
            string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }
        }
    }
}
