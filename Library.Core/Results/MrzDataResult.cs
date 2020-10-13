using Library.Core.Model.Entities;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines MRZData Result for data from MRZData table
    /// </summary>
    public class MrzDataResult
    {
        /// <summary>
        /// Initialize new instance of <see cref="MrzDataResult"/>
        /// </summary>
        /// <param name="mrzData">MrzData entity</param>
        public MrzDataResult(Mrzdata mrzData)
        {
            Id = mrzData.Id;
            FirstRow = mrzData.FirstRow;
            SecondRow = mrzData.SecondRow;
            ThirdRow = mrzData.ThirdRow;
            IsDOBvalid = mrzData.Dobvalid;
            IsCardNumberValid = mrzData.CardNumberValid;
            IsDOEvalid = mrzData.Doevalid;
            CompositeCheckValid = mrzData.CompositeCheckValid;
        }

        public int Id { get; set; }
        public string FirstRow { get; set; }
        public string SecondRow { get; set; }
        public string ThirdRow { get; set; }
        public bool? IsDOBvalid { get; set; }
        public bool? IsCardNumberValid { get; set; }
        public bool? IsDOEvalid { get; set; }
        public bool? CompositeCheckValid { get; set; }
    }
}
