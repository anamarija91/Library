using Library.Core.Exceptions;
using Library.Core.Requests;
using Library.Core.Results;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines Microblink client interface
    /// </summary>
    public interface IMicroblinkClientService
    {
        /// <summary>
        /// Calls GetInfo from microblink Client
        /// </summary>
        /// <returns>Returns result from GetInfo call as string</returns>
        Task<string> GetInfo();

        /// <summary>
        /// Cals MRTD recognizer from microblink client. If rawMrzString is empty throws <see cref="MicroblinkClientException"/>
        /// </summary>
        /// <param name="request">Image request</param>
        /// <returns>Returns OCR data result as <see cref="MrzParserResult"/></returns>
        Task<MrzParserResult> CallMRTDRecognizer(ImageRequest request);
    }
}
