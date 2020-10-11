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
    /// Defines rental controller.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RentalsController 
        : ControllerBase
    {
        private readonly IRentalService rentalService;

        /// <summary>
        /// Initialize a new instance of <see cref="RentalsController"/> class.
        /// </summary>
        /// <param name="rentalService">Rental service.</param>
        public RentalsController(IRentalService rentalService)
        {
            this.rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
        }

        /// <summary>
        /// Get all rentals.
        /// </summary>
        /// <returns>Returns list of rentals as <see cref="RentalResult"/></returns>
        [HttpGet]
        [Route("")]
        [Produces(typeof(IEnumerable<RentalResult>))]
        public async Task<IActionResult> GetAll()
        {
            var users = await rentalService.GetAll();

            return Ok(users);
        }

        /// <summary>
        /// Gets rental by id.
        /// </summary>
        /// <param name="id">Rental id</param>
        /// <returns>Returns rental as <see cref="RentalResult"/></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(RentalResult))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var rental = await rentalService.GetById(id);

            return Ok(rental);
        }

        /// <summary>
        /// Creates new rental / Borrows new book copy
        /// </summary>
        /// <param name="request">Rental create request as <see cref="CreateRentalRequest"/></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Produces(typeof(CreatedAtActionResult))]
        public async Task<IActionResult> Post([FromBody] CreateRentalRequest request)
        {
            var rental = await rentalService.CreateRentalEvent(request);

            return CreatedAtAction(nameof(Get), new { id = rental.Id }, null);
        }

        /// <summary>
        /// Finds top N users by Total Overdue
        /// </summary>
        /// <param name="numberOfTopUsers">N</param>
        /// <returns>Returns list of users with their overdue as <see cref="OverdueResult"/></returns>
        [HttpGet]
        [Route("TopOverdues")]
        [Produces(typeof(IEnumerable<OverdueResult>))]
        public async Task<IActionResult> GetTopUsersOverdueTimes([FromQuery] int numberOfTopUsers)
        {
            var topOverdues = await rentalService.GetTopUsersOverdueTimes(numberOfTopUsers);

            return Ok(topOverdues);
        }

        /// <summary>
        /// Update rentals with date of returned book for user and book copy
        /// </summary>
        /// <param name="request">Update request with UserId, BookCopyId and ReturnDate</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("ReturnBookCopy")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> ReturnBookCopy([FromBody] PatchRentalRequest request)
        {
            await rentalService.ReturnBookCopy(request);

            return NoContent();
        }
    }
}
