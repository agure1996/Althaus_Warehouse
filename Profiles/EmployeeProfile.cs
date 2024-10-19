using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Mappings
{
    /// <summary>
    /// AutoMapper profile for employee mappings
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map from Employee entity to EmployeeDTO for GET requests.
            CreateMap<Employee, EmployeeDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")) // Combine names
               .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.DateHired)) 
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeType.ToString())) // Map EmployeeType to string
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive)) 
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Map email


            // Map from CreateEmployeeDTO to Employee entity for POST/Create requests.
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => Enum.Parse<EmployeeType>(src.EmployeeType)))
                // Ensure string to enum conversion for EmployeeType
                .AfterMap((src, dest) => dest.PasswordHash = HashPassword(src.Password)); // Use AfterMap to hash the password after mapping

            // Map from UpdateEmployeeDTO to Employee entity for PUT/Update requests.
            CreateMap<UpdateEmployeeDTO, Employee>()
                .ForMember(dest => dest.DateHired, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrEmpty(src.Password))
                    {
                        dest.PasswordHash = HashPassword(src.Password); // Only hash and set password if provided
                    }
                });

            // Helper function to hash passwords
            string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password); // Hash password using BCrypt
            }
        }
    }
}
