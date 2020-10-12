using Library.Core.Exceptions;
using Library.Core.Requests;
using Library.Core.Results;
using System.Threading.Tasks;

namespace Library.Core.Services
{
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
        /// <returns>Returns rawMrzString data result as <see cref="MrzDataResult"/></returns>
        Task<MrzDataResult> CallMRTDRecognizer(ImageRequest request);
    }
}
