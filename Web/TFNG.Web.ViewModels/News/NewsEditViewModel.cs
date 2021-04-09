namespace TFNG.Web.ViewModels.News
{
    using Microsoft.AspNetCore.Http;
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class NewsEditViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IFormFile Picture { get; set; }

        public string LatinTitle { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}
