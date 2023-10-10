namespace CRUD_WebApi.WebApi.Services.JwtAuthentication
{
    public interface ITokenService
    {
        public string CreateToken(int id);
    }
}
