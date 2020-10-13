using Library.Core.Model.Entities;
using Library.Core.Results;
using Library.Core.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines MRZDataService class
    /// </summary>
    public class MrzDataService : ServiceBase, IMRZDataService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="MrzDataService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public MrzDataService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <inheritdoc />
        public async Task<MrzDataResult> CreateMrzData(MrzParserResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var mrzDataEntity = new Mrzdata
            {
                FirstRow = result.FirstRow,
                SecondRow = result.SecondRow,
                ThirdRow = result.ThirdRow,
                Dobvalid = result.IsDOBValid,
                CardNumberValid = result.IsCardNumberValid,
                Doevalid = result.IsDOEValid,
                CompositeCheckValid = result.IsCompositeCheckValid
            };

            await UnitOfWork.MrzData.Add(mrzDataEntity);
            await UnitOfWork.Commit();

            return new MrzDataResult(mrzDataEntity);
        }
    }
}
