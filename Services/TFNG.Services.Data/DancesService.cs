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

    public class DancesService : IDancesService
    {
        private readonly IDeletableEntityRepository<Dance> danceRepository;

        public DancesService(IDeletableEntityRepository<Dance> danceRepository)
        {
            this.danceRepository = danceRepository;
        }

        public async Task<int> CreateAsync(string name, string description, string userId, string imageUrl, string latinName, string folkloreArea, string videoUrl)
        {
            var dance = new Dance
            {
                Name = name,
                Description = description,
                UserId = userId,
                ImageUrl = imageUrl.Insert(54, "c_fit,h_600,w_1400/"),
                LatinName = latinName,
                FolkloreArea = folkloreArea,
                VideoUrl = videoUrl,
            };

            await this.danceRepository.AddAsync(dance);
            await this.danceRepository.SaveChangesAsync();

            return dance.Id;
        }

        public async Task<int> EditAsync(string name, string description, string userId, string imageUrl, string latinName, string folkloreArea, string videoUrl, int id)
        {
            var dance = await this.danceRepository
               .All()
               .FirstOrDefaultAsync(l => l.Id == id);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                dance.ImageUrl = imageUrl.Insert(54, "c_fit,h_600,w_1400/");
            }

            dance.Name = name;
            dance.Description = description;
            dance.UserId = userId;
            dance.LatinName = latinName;
            dance.FolkloreArea = folkloreArea;
            dance.VideoUrl = videoUrl;

            await this.danceRepository.SaveChangesAsync();

            return dance.Id;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var danceViewModel = await this.danceRepository
                .All()
                .Where(l => l.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return danceViewModel;
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

        public async Task DeleteByIdAsync(int id)
        {
            var dance = await this.danceRepository.All().FirstOrDefaultAsync(l => l.Id == id);

            dance.IsDeleted = true;
            dance.DeletedOn = DateTime.UtcNow;
            this.danceRepository.Update(dance);
            await this.danceRepository.SaveChangesAsync();
        }
    }
}
