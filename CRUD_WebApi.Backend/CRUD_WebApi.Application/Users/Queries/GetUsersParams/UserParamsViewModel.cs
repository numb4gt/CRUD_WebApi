using AutoMapper;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersParams
{
    public class UserParamsViewModel : IMapWith<Domain.User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<string> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserParamsViewModel>()
                .ForMember(userVM => userVM.Name,
                    opt => opt.MapFrom(user => user.Name))
                .ForMember(userVM => userVM.Age,
                    opt => opt.MapFrom(user => user.Age))
                .ForMember(userVM => userVM.Email,
                    opt => opt.MapFrom(user => user.Email))
                .ForMember(userVM => userVM.Id,
                    opt => opt.MapFrom(user => user.Id))
                .ForMember(userVM => userVM.Roles, 
                    opt => opt.MapFrom(user => user.Roles
                    .Select(role => role.Role.ToString())
                    .ToList()));
        }
    }
}
