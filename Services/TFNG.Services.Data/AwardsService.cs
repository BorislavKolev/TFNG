namespace TFNG.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<int> CreateAsync(string name, string latinName, DateTime datetime, string location, string place, string imageUrl, string userId)
        {
            var award = new Award
            {
                Name = name,
                Date = datetime,
                UserId = userId,
                ImageUrl = imageUrl.Insert(54, "c_fill,h_493,w_690/"),
                LatinName = latinName,
                Location = location,
                Place = place,
            };

            await this.awardsRepository.AddAsync(award);
            await this.awardsRepository.SaveChangesAsync();

            return award.Id;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var danceViewModel = await this.awardsRepository
                .All()
                .Where(l => l.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return danceViewModel;
        }

        public async Task<int> EditAsync(string name, string latinName, DateTime datetime, string location, string place, string imageUrl, string userId, int id)
        {
            var award = await this.awardsRepository
               .All()
               .FirstOrDefaultAsync(l => l.Id == id);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                award.ImageUrl = imageUrl.Insert(54, "c_fill,h_493,w_690/");
            }

            award.Name = name;
            award.UserId = userId;
            award.LatinName = latinName;
            award.Location = location;
            award.Place = place;
            award.Date = datetime;

            await this.awardsRepository.SaveChangesAsync();

            return award.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var award = await this.awardsRepository.All().FirstOrDefaultAsync(l => l.Id == id);

            award.IsDeleted = true;
            award.DeletedOn = DateTime.UtcNow;
            this.awardsRepository.Update(award);
            await this.awardsRepository.SaveChangesAsync();
        }
    }
}
