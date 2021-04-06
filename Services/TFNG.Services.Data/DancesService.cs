namespace TFNG.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TFNG.Data.Common.Repositories;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Services.Mapping;

    public class DancesService : IDancesService
    {
        private readonly IDeletableEntityRepository<Dance> danceRepository;

        public DancesService(IDeletableEntityRepository<Dance> danceRepository)
        {
            this.danceRepository = danceRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            var query = this.danceRepository
               .All()
               .OrderByDescending(x => x.CreatedOn)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var dance = this.danceRepository.All().Where(x => x.LatinName == name).To<T>().FirstOrDefault();

            return dance;
        }


        public int GetDancesCount()
        {
            return this.danceRepository.All().Count();
        }
    }
}
