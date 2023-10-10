using AutoMapper;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Application.Users.Commands.Create;
using CRUD_WebApi.Application.Users.Commands.Grant;

namespace CRUD_WebApi.WebApi.Models
{
    public class GrantUserDto : IMapWith<GrantUserCommand>
    {
        public int UserId { get; set; }
        public string UserRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GrantUserDto, GrantUserCommand>()
                .ForMember(userCommand => userCommand.UserId,
                    opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(userCommand => userCommand.Role,
                    opt => opt.MapFrom(dto => dto.UserRole));
        }
    }
}
