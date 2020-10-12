using Library.Core.Model.Entities;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;

namespace Library.Infrastructure.SieveProcessors
{
    /// <summary>
    /// Defines sieve processor for library
    /// </summary>
    public class LibrarySieveProcessor
        : SieveProcessor
    {
        /// <summary>
        /// Initialize new instance of <see cref="LibrarySieveProcessor"/> class.
        /// </summary>
        /// <param name="options"></param>
        public LibrarySieveProcessor(IOptions<SieveOptions> options)
            : base(options)
        {
        }

        /// <summary>
        /// Define property mappings
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            mapper = MapUserProperties(mapper);


            return base.MapProperties(mapper);
        }

        /// <summary>
        /// Define property mapping for <see cref="User"/>
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        private SievePropertyMapper MapUserProperties(SievePropertyMapper mapper)
        {
            _ = mapper.Property<User>(u => u.Email).CanFilter().CanSort();
            _ = mapper.Property<User>(u => u.FirstName).CanFilter().CanSort();
            _ = mapper.Property<User>(u => u.LastName).CanFilter().CanSort();

            return mapper;
        }
    }
}
