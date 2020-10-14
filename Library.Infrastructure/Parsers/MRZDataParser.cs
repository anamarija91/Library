using Library.Core.Exceptions;
using Library.Core.Parsers;
using Library.Core.Results;
using System;
using System.Linq;

namespace Library.Infrastructure.Parsers
{
    /// <summary>
    /// Defines MRZDataparser class for parsing data from MRTD recognizer
    /// </summary>
    public class MRZDataParser : IMRZDataParser, IParser<MrzParserResult>
    {
        private readonly string rawString;

        /// <summary>
        /// Initialize new instance of <see cref="MRZDataParser"/>
        /// </summary>
        /// <param name="rawString">Raw string from MRTD recognizer</param>
        public MRZDataParser(string rawString)
        {
            this.rawString = rawString;
        }

        /// <inheritdoc/>
        public MrzParserResult ReadAndValidateBackSideData()
        {
            var result = ParseData();

            result.IsCompositeCheckValid = ValidateCompositeCheck(result.FirstRow, result.SecondRow);

            return result;
        }

        /// <inheritdoc/>
        public MrzParserResult ParseData()
        {
            var rows = rawString.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length != 3)
            {
                throw new MicroblinkClientException("rawMrzString data not valid.There should be 3 rows of length 30.");
            }

            var result = new MrzParserResult
            {
                FirstRow = rows[0],
                SecondRow = rows[1],
                ThirdRow = rows[2]
            };

            if (result.FirstRow.Length != 30 || result.SecondRow.Length != 30 || result.ThirdRow.Length != 30)
            {
                throw new MicroblinkClientException("rawMrzString data not valid. Rows should have 30 characters.");
            }

            var cardNumberValidModel = ParseAndValidateCardNumber(result.FirstRow);

            result.CardNumber = cardNumberValidModel.Number;
            result.IsCardNumberValid = cardNumberValidModel.IsValid;

            var dobValidModel = ParseAndValidateDOB(result.SecondRow);

            result.DOB = dobValidModel.Number;
            result.IsDOBValid = dobValidModel.IsValid;

            var doeValidModel = ParseAndValidateDOE(result.SecondRow);

            result.DOE = doeValidModel.Number;
            result.IsDOEValid = doeValidModel.IsValid;

            result.LastName = GetLastName(result.ThirdRow);

            result.FirstName = GetFirstName(result.ThirdRow);

            return result;
        }

        #region Field Helpers

        /// <summary>
        /// Get part of rawData that defines cardNumber
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCardNumber(string row)
        {
            return row.Substring(MRZDataParserConstants.CardNumberStartIndex, MRZDataParserConstants.CardNumberLength);
        }

        /// <summary>
        /// Gets CardNumber check digit
        /// </summary>
        /// <param name="row">Row containg cardNumber</param>
        /// <returns></returns>
        private string GetCardNumberCheckDigit(string row)
        {
            return row.Substring(MRZDataParserConstants.CardNumberStartIndex + MRZDataParserConstants.CardNumberLength, 1);
        }

        /// <summary>
        /// Get part of rawData that defines DateOfBirth
        /// </summary>
        /// <param name="row">Row containg  dob</param>
        /// <returns></returns>
        private string GetDOB(string row)
        {
            return row.Substring(MRZDataParserConstants.DOBStartIndex, MRZDataParserConstants.DatesLength);
        }

        /// <summary>
        /// Get DateOfBirth check digit
        /// </summary>
        /// <param name="row">Row containg  dob</param>
        /// <returns></returns>
        private string GetDOBCheckDigit(string row)
        {
            return row.Substring(MRZDataParserConstants.DOBStartIndex + MRZDataParserConstants.DatesLength, 1);
        }

        /// <summary>
        /// Get part of rawData that defines DateOfExpiry
        /// </summary>
        /// <param name="row">Row containg doe</param>
        /// <returns></returns>
        private string GetDOE(string row)
        {
            return row.Substring(MRZDataParserConstants.DOEStartIndex, MRZDataParserConstants.DatesLength);
        }

        /// <summary>
        /// Get DateOfExpiry check digit
        /// </summary>
        /// <param name="row">Row containg doe</param>
        /// <returns></returns>
        public string GetDOECheckDigit(string row)
        {
            return row.Substring(MRZDataParserConstants.DOEStartIndex + MRZDataParserConstants.DatesLength, 1);
        }

        /// <summary>
        /// Gets data for composite holder
        /// </summary>
        /// <param name="row1">first data row</param>
        /// <param name="row2">second data row</param>
        /// <returns></returns>
        public string GetCompositeHolder(string row1, string row2)
        {
            return
                row1.Substring(MRZDataParserConstants.CardNumberStartIndex)
                + GetDOB(row2) + GetDOBCheckDigit(row2)
                + GetDOE(row2) + GetDOECheckDigit(row2)
                + row2.Substring(MRZDataParserConstants.SecondRowFillerStartIndex);
        }

        /// <summary>
        /// Get last name from third row of rawMrzString
        /// </summary>
        /// <param name="row">Row containing Last Name</param>
        /// <returns></returns>
        private string GetLastName(string row)
        {
            var endIndex = row.IndexOf(MRZDataParserConstants.DoubleFiller); // end just before '<<'
            var lastName = row[..endIndex]; // Range.EndAt(new Index(3, fromEnd: false))

            return lastName.Replace(MRZDataParserConstants.FillerCharacter, ' '); // Replace filler with space if there is more than one name
        }

        /// <summary>
        /// Gets first name from third row of rawMrzString
        /// </summary>
        /// <param name="row">Row containing FirstName</param>
        /// <returns></returns>
        private string GetFirstName(string row)
        {
            var startIndex = row.IndexOf(MRZDataParserConstants.DoubleFiller) + 1; // starts just after '<<'

            var firstName = row.Substring(startIndex);

            firstName = firstName.Replace(MRZDataParserConstants.FillerCharacter, ' ');

            return firstName.TrimEnd();
        }

        #endregion

        #region NumberValidation

        /// <summary>
        /// Parse card number and checks if number is valid
        /// </summary>
        /// <param name="row">Row containing cardNumber </param>
        /// <returns>Returns number and valid state as <see cref="NumberValidModel"/></returns>
        private NumberValidModel ParseAndValidateCardNumber(string row)
        {
            var cardNumber = GetCardNumber(row);

            return new NumberValidModel
            {
                Number = cardNumber,
                IsValid = ValidateNumber(cardNumber + GetCardNumberCheckDigit(row))
            };
        }

        /// <summary>
        /// Parse dateOfBirth and checks if number is valid
        /// </summary>
        /// <param name="row">Row containing dob </param>
        /// <returns>Returns number and valid state as <see cref="NumberValidModel"/></returns>
        public NumberValidModel ParseAndValidateDOB(string row)
        {
            var dob = GetDOB(row);

            return new NumberValidModel
            {
                Number = dob,
                IsValid = ValidateNumber(dob + GetDOBCheckDigit(row))
            };
        }

        /// <summary>
        /// Parse doe and checks if number is valid
        /// </summary>
        /// <param name="row">Row containing doe </param>
        /// <returns>Returns number and valid state as <see cref="NumberValidModel"/></returns>
        public NumberValidModel ParseAndValidateDOE(string row)
        {
            var doe = GetDOE(row);

            return new NumberValidModel
            {
                Number = doe,
                IsValid = ValidateNumber(doe + GetDOECheckDigit(row))
            };
        }

        /// <summary>
        /// Validates composite check digits
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        public bool ValidateCompositeCheck(string row1, string row2)
        {
            var compositeHolder = GetCompositeHolder(row1, row2);

            return ValidateNumber(compositeHolder);
        }
        #endregion

        #region CheckDigits Algorithm

        /// <summary>
        /// Finds single digit by check digits algorithm:
        /// Step 1. Going from left to right, multiply each digit of the pertinent numerical data element by the weighting figure appearing in the corresponding sequential position.
        /// Step 2. Add the products of each multiplication.
        /// Step 3. Divide the sum by 10 (the modulus).
        /// Step 4. The remainder shall be the check digit.
        /// </summary>
        /// <param name="field">Alphanumreic field with filler character </param>
        /// <returns></returns>
        public int CheckDigits(string field)
        {
            var sum = 0;

            for (int i = 0; i < field.Length; i++)
            {
                sum += GetWeight(i) * GetCharacterValue(field.ElementAt(i));
            }

            return sum % 10;
        }

        /// <summary>
        /// Validates given field and checks if it's last number has the same value as the one calculated with CheckDigits algorithn
        /// </summary>
        /// <param name="fieldData">Field data</param>
        /// <returns></returns>
        private bool ValidateNumber(string fieldData)
        {
            var length = fieldData.Length;

            var cardNumber = fieldData.Substring(0, length - 1);

            var checkDigit = char.GetNumericValue(fieldData[^1]); // last element  new Index(1, fromEnd: true)

            return checkDigit == CheckDigits(cardNumber);
        }

        /// <summary>
        /// Returns character value
        ///     - Numeric character it's numeric value
        ///     - A-Z have values 10 - 35
        ///     - filler has value 0
        /// </summary>
        /// <param name="character">Character</param>
        /// <returns></returns>
        private int GetCharacterValue(char character)
        {
            var offset = 'A' - 10; // offset from ASCII representation - 55

            return true switch
            {
                bool _ when Char.IsNumber(character) => (int)char.GetNumericValue(character),
                bool _ when Char.IsUpper(character) => character - offset,
                bool _ when character.Equals(MRZDataParserConstants.FillerCharacter) => 0,
                _ => throw new MicroblinkClientException($"Character {character} is not valid.")
            };
        }

        /// <summary>
        /// Returns weight of digit defined by it's index
        /// Digits are repetitive wwighed by weight1, weight2, weight3 (7,3,1)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetWeight(int index)
        {
            return (index % 3) switch
            {
                0 => MRZDataParserConstants.Weight1,
                1 => MRZDataParserConstants.Weight2,
                2 => MRZDataParserConstants.Weight3,
                _ => 0, // will not happen
            };
        }

        #endregion
    }
}
