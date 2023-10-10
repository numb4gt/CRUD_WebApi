using AutoMapper;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Accounts.Queries.Authentication
{
    public class AuthenticationDto : IMapWith<Account>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, AuthenticationDto>()
               .ForMember(dto => dto.Id,
                   opt => opt.MapFrom(user => user.Id))
               .ForMember(dto => dto.Login,
                   opt => opt.MapFrom(user => user.Login))
               .ForMember(dto => dto.Password,
                   opt => opt.MapFrom(user => user.Password));
        }
    }
}
