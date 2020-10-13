namespace Library.Core.Parsers
{
    /// <summary>
    /// Defines Parser factory interface
    /// </summary>
    public interface IParserFactory
    {
        /// <summary>
        /// Creates a parser to read data for string
        /// </summary>
        /// <param name="rawString">rawString</param>
        /// <returns>Parser</returns>
        IMRZDataParser CreateDataParser(string rawString);
    }
}
