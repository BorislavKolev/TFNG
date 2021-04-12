namespace TFNG.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TFNG.Data.Common.Repositories;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Services.Mapping;

    public class GalleryService : IGalleryService
    {
        private readonly IDeletableEntityRepository<Picture> pictureRepository;

        public GalleryService(IDeletableEntityRepository<Picture> pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        public int GetCompetitionPicturesCount()
        {
            return this.pictureRepository.All().Where(x => x.Type == "Competition").Count();
        }

        public IEnumerable<T> GetAllCompetition<T>(int? take = null, int skip = 0)
        {
            var query = this.pictureRepository
               .All()
               .Where(x => x.Type == "Competition")
               .OrderByDescending(x => x.CreatedOn)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetVacationPicturesCount()
        {
            return this.pictureRepository.All().Where(x => x.Type == "Vacation").Count();
        }

        public IEnumerable<T> GetAllVacation<T>(int? take = null, int skip = 0)
        {
            var query = this.pictureRepository
               .All()
               .Where(x => x.Type == "Vacation")
               .OrderByDescending(x => x.CreatedOn)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
