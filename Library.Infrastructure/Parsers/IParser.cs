namespace Library.Infrastructure.Parsers
{
    public interface IParser<T>
    {
        /// <summary>
        /// Read parsable data for a generic model <typeparamref name="T"/>
        /// </summary>
        /// <returns>Models with parsed data</returns>
        T ParseData();
    }
}
