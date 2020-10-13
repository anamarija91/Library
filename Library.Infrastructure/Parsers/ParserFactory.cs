using Library.Core.Parsers;

namespace Library.Infrastructure.Parsers
{
    /// <summary>
    /// Defines parser factory class
    /// </summary>
    public class ParserFactory : IParserFactory
    {
        /// <summary>
        /// Creates Data Parser
        /// </summary>
        /// <param name="rawString">Raw string</param>
        /// <returns></returns>
        public IMRZDataParser CreateDataParser(string rawString)
        {
            return new MRZDataParser(rawString);
        }
    }
}
