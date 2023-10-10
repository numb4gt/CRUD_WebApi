using AutoMapper;
using CRUD_WebApi.Application.Accounts.Commands.Registration;
using CRUD_WebApi.Application.Accounts.Queries.Authentication;
using CRUD_WebApi.WebApi.Models;
using CRUD_WebApi.WebApi.Services.JwtAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthController(IMapper mapper, ITokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
        }

        /// <summary>
        /// For login Account
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// POST /login
        /// 
        /// ### Example Data: ###
        ///     {
        ///         "Login":"Morning123",
        ///         "Password":"12345678"
        ///     }
        /// </remarks>
        /// <param name="authenticationUserDto"></param>
        /// <returns>Token</returns>
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<string>> Auth([FromBody] AuthenticationAccDto authenticationUserDto)
        {
            Console.WriteLine("Bless");
            var query = _mapper.Map<AuthenticationQuery>(authenticationUserDto);
            var userDto = await Mediator.Send(query);

            var token = _tokenService.CreateToken(userDto.Id);
            return Ok(token);
        }

        /// <summary>
        /// For registration Account
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// POST /register
        /// 
        /// ### Example Data: ###
        ///     {
        ///         "Login":"Morning123",
        ///         "Password":"12345678",
        ///         "Email":"bless@mail.ru"
        ///     }
        /// </remarks>
        /// <param name="registrationUserDto"></param>
        /// <returns>Ok</returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegistrationAccDto registrationUserDto)
        {
            Console.WriteLine("Bless");
            var query = _mapper.Map<RegistrationAccountCommand>(registrationUserDto);
            var user = await Mediator.Send(query);
            return Ok();
        }
    }
}
