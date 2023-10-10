using AutoMapper;
using CRUD_WebApi.Application.Accounts.Queries.Authentication;
using CRUD_WebApi.Application.Common.Mappings;

namespace CRUD_WebApi.WebApi.Models
{
    public class AuthenticationAccDto : IMapWith<AuthenticationQuery>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthenticationAccDto, AuthenticationQuery>()
                .ForMember(validateQuery => validateQuery.Login,
                    opt => opt.MapFrom(dto => dto.Login))
                .ForMember(validateQuery => validateQuery.Password,
                    opt => opt.MapFrom(dto => dto.Password));

        }
    }
}
