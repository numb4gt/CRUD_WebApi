using AutoMapper;
using CRUD_WebApi.Application.Accounts.Commands.Registration;
using CRUD_WebApi.Application.Common.Mappings;

namespace CRUD_WebApi.WebApi.Models
{
    public class RegistrationAccDto : IMapWith<RegistrationAccountCommand>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegistrationAccDto, RegistrationAccountCommand>()
            .ForMember(registerCommand => registerCommand.Login,
                opt => opt.MapFrom(dto => dto.Login))
            .ForMember(registerCommand => registerCommand.Password,
                opt => opt.MapFrom(dto => dto.Password))
            .ForMember(registerCommand => registerCommand.Email,
                opt => opt.MapFrom(dto => dto.Email));
        }
    }
}
