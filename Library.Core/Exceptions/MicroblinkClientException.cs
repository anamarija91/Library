namespace Library.Core.Exceptions
{

    public class MicroblinkClientException
        : CustomException
    {
        /// <summary>
        /// Initailizes new instance of <see cref="MicroblinkClientException"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        public MicroblinkClientException(string message) : base(400, "MicroblinkClientResponseNotValid", message) { }
    }
}
