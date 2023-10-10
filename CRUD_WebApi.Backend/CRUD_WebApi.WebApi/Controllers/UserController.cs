using AutoMapper;
using CRUD_WebApi.Application.Users.Commands.Create;
using CRUD_WebApi.Application.Users.Commands.Delete;
using CRUD_WebApi.Application.Users.Commands.Grant;
using CRUD_WebApi.Application.Users.Commands.Update;
using CRUD_WebApi.Application.Users.Queries.GetUsersList;
using CRUD_WebApi.Application.Users.Queries.GetUsersParams;
using CRUD_WebApi.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_WebApi.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get user list
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// GET /user
        /// 
        /// ### Features ###
        /// If you don't use the parameters, 
        /// the normal ones will be treated as
        /// 
        /// 1)filtration is skipped
        /// 
        /// 2)sortBy=id, direction=asc
        /// 
        /// 3)page=1, pageSize = 50
        /// 
        /// ### Available values for filtering: ###
        /// 
        /// filterBy => ("age", "email", "name", "role")
        /// 
        /// paramToFilter => 
        /// 
        ///    -   if use "age" can be only positive number
        /// 
        ///    -   if use "role" can be only {"Admin", "SuperAdmin", "Support", "User"}
        ///                  
        ///    -   if use "name, email" can be any string (case sensitive search)
        /// 
        /// ### Available values for sorting: ###
        /// 
        /// sortBy => ("age", "email", "name")
        /// 
        /// direction => ("desc", "asc")
        /// </remarks>
        /// <param name="sortBy"></param>
        /// <param name="direction"></param>
        /// <param name="filterBy"></param>
        /// <param name="paramToFilter"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>Returns UserListViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If you try to add invalid role or string in age in filtration</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserListViewModel>> GetAll(string? sortBy, string? direction, string? filterBy, 
            string? paramToFilter, int page = 1, int pageSize = 50)
        {
            var query = new GetUserListQuery{
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                Direction = direction,
                FilterBy = filterBy,
                ParamToFilter = paramToFilter
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// GET /user/{id}
        /// 
        /// ### Features ###
        /// 
        /// If the user is not detected - 404NotFound
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>UserParamsViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If you try to get non-existent user</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserParamsViewModel>> GetUser(int id)
        {

            var query = new GetUserParamsQuery
            {
                Id = id
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// POST /user
        /// 
        /// ### Example Data: ###
        /// 
        ///     {
        ///         "Name":"Jone",
        ///         "Email":"jonehalib@mail.ru",
        ///         "Age": 25,
        ///         "Roles":["Admin", "User"]
        ///     }
        ///     
        /// ### Features ###
        /// 
        /// - "Roles" can be only with {"Admin", "SuperAdmin", "Support", "User"}
        /// - "Age" is a positive number
        /// - "Email" must be valid and unique  
        /// </remarks>
        /// <param name="createUserDto"></param>
        /// <returns>new UserId</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If you try to add invalid data or existing email address</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        /// <summary>
        /// Add new role to user
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// POST /user/grant
        /// 
        /// ### Example Data: ###
        ///     {
        ///         "UserId": "1",
        ///         "UserRole": "SuperAdmin"
        ///     }
        ///     
        /// ### Features ###
        /// 
        /// - Assigning a new role is done by "UserId"
        /// - "UserRole" can be only {"Admin", "SuperAdmin", "Support", "User"}
        /// 
        /// </remarks>
        /// <param name="grantUserDto"></param>
        /// <response code="200">Success</response>
        /// <response code="400">If you try to add existing role to user or invalid data</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If you try add role for non-existent user</response>
        /// <returns>new grantId</returns>
        [Route("grant")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Grant([FromBody] GrantUserDto grantUserDto)
        {
            var command = _mapper.Map<GrantUserCommand>(grantUserDto);
            var grantId = await Mediator.Send(command);
            return Ok(grantId);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// PUT /user
        /// 
        /// ### Example Data: ###
        ///     {
        ///         "Id":"1",
        ///         "Name":"Pol",
        ///         "Email":"polhalib@mail.ru",
        ///         "Age": 24,
        ///         "Roles":["Admin", "User"]
        ///     }
        ///     
        /// ### Features ###
        /// 
        /// - Update is done by "Id"
        /// - "Roles" can be only with {"Admin", "SuperAdmin", "Support", "User"}
        /// - "Age" is a positive number
        /// - "Email" must be valid and unique 
        /// </remarks>
        /// <param name="updateUserDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If you try to add invalid data or existing email address</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If you try to update non-existent user</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            var userId = await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>
        /// ### Sample request: ###
        /// 
        /// DELETE /user/{id}
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If you try to delete non-existent user</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand
            {
                Id = id,
            };

            await Mediator.Send(command);
            return NoContent();

        }
    }
}
