using Library.Core.Results;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Defines book titles controller.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class BookTitlesController
        : ControllerBase
    {
        private readonly IBookTitlesService bookTitleService;

        /// <summary>
        /// Initializes new instance of book titles controller.
        /// </summary>
        /// <param name="bookTitlesService"></param>
        public BookTitlesController(IBookTitlesService bookTitlesService)
        {
            this.bookTitleService = bookTitlesService ?? throw new ArgumentNullException(nameof(bookTitlesService));
        }

        /// <summary>
        /// Gets BookTitle rent details
        /// </summary>
        /// <param name="bookTitleId">Book Title Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{bookTitleId}/TitleRentals")]
        [Produces(typeof(RentalResult))]
        public async Task<IActionResult> GetBookTitleRentalDetails([FromRoute] int bookTitleId)
        {
            var result = await bookTitleService.GetBookTitleRentalDetails(bookTitleId);

            return Ok(result);
        }
    }
}
