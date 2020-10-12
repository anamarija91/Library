using Microsoft.AspNetCore.Http;

namespace Library.Core.Requests
{
    /// <summary>
    /// Defines Image Request.
    /// </summary>
    public class ImageRequest
    {
        public IFormFile Image { get; set; }
    }
}
