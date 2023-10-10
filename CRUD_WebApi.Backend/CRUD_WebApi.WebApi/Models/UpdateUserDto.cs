using AutoMapper;
using CRUD_WebApi.Application.Users.Commands.Create;
using CRUD_WebApi.Application.Users.Commands.Update;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Domain;

namespace CRUD_WebApi.WebApi.Models
{
    public class UpdateUserDto : IMapWith<CreateUserCommand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<string> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(userCommand => userCommand.Id,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(userCommand => userCommand.Name,
                    opt => opt.MapFrom(dto => dto.Name))
                .ForMember(userCommand => userCommand.Email,
                    opt => opt.MapFrom(dto => dto.Email))
                .ForMember(userCommand => userCommand.Age,
                    opt => opt.MapFrom(dto => dto.Age))
                .ForMember(userCommand => userCommand.Roles,
                    opt => opt.MapFrom(dto => dto.Roles));
        }
    }
}
