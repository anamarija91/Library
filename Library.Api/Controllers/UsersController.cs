using Library.Core.Model;
using Library.Core.Requests;
using Library.Core.Results;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Defines user controller.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UsersController
        : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initialize a new instance of <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">Users service.</param>
        public UsersController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="model">Filtering model from query <see cref="PagingFilteringModel"/></param>
        /// <returns>Returns list of users as <see cref="UserResult"/></returns>
        [HttpGet]
        [Route("")]
        [Produces(typeof(IEnumerable<UserResult>))]
        public async Task<IActionResult> GetAll([FromQuery] PagingFilteringModel model)
        {
            var users = await userService.GetAll(model);

            return Ok(users);
        }

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns user as <see cref="UserResult"/></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(UserResult))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await userService.GetById(id);

            return Ok(user);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="request">User create request as <see cref="UserCreateRequest"/></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Produces(typeof(CreatedAtActionResult))]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            var user = await userService.Create(request);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, null);
        }

        /// <summary>
        /// Creates new user from MRTD and saves MRTD data
        /// </summary>
        /// <param name="request">Image request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/MRTD")]
        [Produces(typeof(CreatedAtActionResult))]
        public async Task<IActionResult> CreateUserWithMRTD([FromForm] ImageRequest request)
        {
            var user = await userService.CreateUserWithMRTD(request);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, null);
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="request">User update request as <see cref="UserUpdateRequest"/></param>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserUpdateRequest request)
        {
            await userService.Update(id, request);

            return NoContent();
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await userService.Delete(id);

            return NoContent();
        }
    }
}
