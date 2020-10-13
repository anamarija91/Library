namespace Library.Infrastructure.Parsers
{
    /// <summary>
    /// Defines MRZData Parser constants
    /// </summary>
    public static class MRZDataParserConstants
    {
        public const char FillerCharacter = '<';
        public const string DoubleFiller = "<<";
        public const int CardNumberStartIndex = 5;
        public const int CardNumberLength = 9;
        public const int DOBStartIndex = 0;
        public const int DOEStartIndex = 8;
        public const int DatesLength = 6;
        public const int SecondRowFillerStartIndex = 18;
        public const int NamesStartIndex = 0;

        public const int Weight1 = 7;
        public const int Weight2 = 3;
        public const int Weight3 = 1;
    }
}
