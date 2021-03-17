namespace TFNG.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TFNG.Data.Common.Repositories;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Services.Mapping;

    public class AwardsService : IAwardsService
    {
        private readonly IDeletableEntityRepository<Award> awardsRepository;

        public AwardsService(IDeletableEntityRepository<Award> awardsRepository)
        {
            this.awardsRepository = awardsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            var query = this.awardsRepository
               .All()
               .OrderByDescending(x => x.Date)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetAwardsCount()
        {
            return this.awardsRepository.All().Count();
        }
    }
}
