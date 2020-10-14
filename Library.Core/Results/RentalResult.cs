using Library.Core.Model.Entities;
using Library.Core.Utils;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines rental result class.
    /// </summary>
    public class RentalResult
    {
        /// <summary>
        /// Initialize new instance of <see cref="RentalResult"/> class.
        /// Copies data from rental entity.
        /// </summary>
        /// <param name="rental">Rent event entity</param>
        public RentalResult(Rental rental)
        {
            Id = rental.Id;
            UserId = rental.UserId;
            BookCopyId = rental.BookCopyId;

            DateRented = rental.DateRented.Date.ToString(ProjectConstants.DateFormat);
            DateDue = rental.DateDue.Date.ToString(ProjectConstants.DateFormat);
            DateReturned = rental.DateReturned?.Date.ToString(ProjectConstants.DateFormat);
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookCopyId { get; set; }

        public string DateRented { get; set; }
        public string DateDue { get; set; }
        public string DateReturned { get; set; }
    }
}
