using Library.Core.Results;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines MRZData service interface
    /// </summary>
    public interface IMRZDataService
    {
        /// <summary>
        /// Creates new MrzData entity from parsed MRTD recognition result
        /// </summary>
        /// <param name="result">MrzParserResult</param>
        /// <returns>Returns created entity as <see cref="MrzDataResult" </returns>
        Task<MrzDataResult> CreateMrzData(MrzParserResult result);
    }
}
