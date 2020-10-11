namespace Library.Core.Requests
{
    /// <summary>
    /// Defines rent request
    /// </summary>
    public class RentalRequest
    {
        public int UserId { get; set; }
        public int BookCopyId { get; set; }
    }

    /// <summary>
    /// Defines rent request for rent event
    /// </summary>
    public class CreateRentalRequest
        : RentalRequest
    {
        public string DateRented { get; set; }
    }

    /// <summary>
    /// Defines rent reqeust for return book event
    /// </summary>
    public class PatchRentalRequest
        : RentalRequest
    {
        public string DateReturned { get; set; }
    }
}
