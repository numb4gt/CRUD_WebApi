using AutoMapper;
using CRUD_WebApi.Application.Users.Commands.Create;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Domain;

namespace CRUD_WebApi.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<string> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.Name,
                    opt => opt.MapFrom(dto => dto.Name))
                .ForMember(userCommand => userCommand.Age,
                    opt => opt.MapFrom(dto => dto.Age))
                .ForMember(userCommand => userCommand.Email,
                    opt => opt.MapFrom(dto => dto.Email))
                .ForMember(userCommand => userCommand.Roles,
                    opt => opt.MapFrom(dto => dto.Roles));
        }
    }
}
