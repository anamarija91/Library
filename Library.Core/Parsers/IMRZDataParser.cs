using Library.Core.Results;

namespace Library.Core.Parsers
{
    /// <summary>
    /// Defines interface for MRZDataParser
    /// </summary>
    public interface IMRZDataParser
    {
        /// <summary>
        /// Read back side identity document data from MRTD recognizer raw string;
        /// Parse data -> FirstName, LastName, DOB
        /// Validates fields containing check digit -> CardNumber, DOB, DOE, Composite
        /// </summary>
        /// <returns></returns>
        MrzParserResult ReadAndValidateBackSideData();
    }
}
