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

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<NewsPost> newsPostsRepository;

        public NewsService(IDeletableEntityRepository<NewsPost> newsPostsRepository)
        {
            this.newsPostsRepository = newsPostsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, string userId, string imageUrl, string latinTitle, string author)
        {
            var newsPost = new NewsPost
            {
                Title = title,
                Content = content,
                UserId = userId,
                ImageUrl = imageUrl.Insert(54, "c_fill,h_768,w_1024/"),
                LatinTitle = latinTitle,
                Author = author,
            };

            await this.newsPostsRepository.AddAsync(newsPost);
            await this.newsPostsRepository.SaveChangesAsync();

            return newsPost.Id;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            var query = this.newsPostsRepository
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
            var newsPost = this.newsPostsRepository.All().Where(x => x.LatinTitle == name).To<T>().FirstOrDefault();

            return newsPost;
        }

        public IEnumerable<T> GetLast<T>(int count)
        {
            var query = this.newsPostsRepository
               .All()
               .OrderByDescending(x => x.CreatedOn)
               .Take(count);

            return query.To<T>().ToList();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var locationViewModel = await this.newsPostsRepository
                .All()
                .Where(l => l.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return locationViewModel;
        }

        public int GetNewsCount()
        {
            return this.newsPostsRepository.All().Count();
        }

        public async Task<int> EditAsync(string title, string content, string userId, string imageUrl, string latinTitle, string author, int id)
        {
            var newsPost = await this.newsPostsRepository
               .All()
               .FirstOrDefaultAsync(l => l.Id == id);

            newsPost.ImageUrl = imageUrl;
            newsPost.Title = title;
            newsPost.Content = content;
            newsPost.UserId = userId;
            newsPost.LatinTitle = latinTitle;
            newsPost.Author = author;

            await this.newsPostsRepository.SaveChangesAsync();

            return newsPost.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var newsPost = await this.newsPostsRepository.All().FirstOrDefaultAsync(l => l.Id == id);

            newsPost.IsDeleted = true;
            newsPost.DeletedOn = DateTime.UtcNow;
            this.newsPostsRepository.Update(newsPost);
            await this.newsPostsRepository.SaveChangesAsync();
        }
    }
}
