using Library.Core.Requests;
using Library.Core.Results;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Defines microblink client controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MicroblinkClientController
        : ControllerBase
    {
        private readonly IMicroblinkClientService microblinkClientService;

        /// <summary>
        /// Initializes new instance of microblink controller.
        /// </summary>
        /// <param name="microblinkClientService">Microblink client service</param>
        public MicroblinkClientController(IMicroblinkClientService microblinkClientService)
        {
            this.microblinkClientService = microblinkClientService ?? throw new ArgumentNullException(nameof(microblinkClientService));
        }

        /// <summary>
        /// Get available recognizers and additional data from microblink cloud api 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Info")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetInfo()
        {
            var result = await microblinkClientService.GetInfo();

            return Ok(result);
        }

        /// <summary>
        /// Calls MRTD recognizer and returns rawMrzString part of response
        /// </summary>
        /// <param name="request">Image request</param>
        /// <returns>Returns mrtd recognizer response as <see cref="MrzParserResult"/>  </returns>
        [HttpPost]
        [Route("Recognizer")]
        public async Task<IActionResult> CallMRTDRecognizer([FromForm] ImageRequest request)
        {
            var result = await microblinkClientService.CallMRTDRecognizer(request);

            return Ok(result);
        }
    }
}
