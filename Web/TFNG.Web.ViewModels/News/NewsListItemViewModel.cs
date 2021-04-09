namespace TFNG.Web.ViewModels.News
{
    using System;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class NewsListItemViewModel : IMapFrom<NewsPost>
    {
        public string Title { get; set; }

        public string LatinTitle { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string NewsPostUrl => $"/News/{this.LatinTitle.Replace(' ', '-')}";
    }
}
