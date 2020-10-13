namespace Library.Core.Results
{
    /// <summary>
    /// Defines MrzParser result class.
    /// </summary>
    public class MrzParserResult
    {
        public string FirstRow { get; set; }
        public string SecondRow { get; set; }
        public string ThirdRow { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CardNumber { get; set; }
        public string DOB { get; set; }
        public string DOE { get; set; }

        public bool IsCardNumberValid { get; set; }
        public bool IsDOBValid { get; set; }
        public bool IsDOEValid { get; set; }
        public bool IsCompositeCheckValid { get; set; }

    }
}
