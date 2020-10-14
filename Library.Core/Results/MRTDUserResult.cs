using Library.Core.Model.Entities;
using Library.Core.Utils;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines MRTD result that is shown with user data
    /// </summary>
    public class MRTDUserResult
    {
        public string CardNumber { get; set; }

        public string DateOfExpiry { get; set; }

        public bool IsCompositeCheckValid { get; set; }

        public MRTDUserResult(Mrzdata mrzdata)
        {
            CardNumber = mrzdata.CardNumber;
            DateOfExpiry = mrzdata.DateOfExpiry.GetValueOrDefault().Date.ToString(ProjectConstants.DateFormat);
            IsCompositeCheckValid = mrzdata.CompositeCheckValid;
        }
    }
}
