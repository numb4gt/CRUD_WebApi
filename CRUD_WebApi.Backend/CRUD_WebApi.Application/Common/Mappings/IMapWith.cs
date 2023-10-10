using AutoMapper;

namespace CRUD_WebApi.Application.Common.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
