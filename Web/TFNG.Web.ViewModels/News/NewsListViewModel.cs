namespace TFNG.Web.ViewModels.News
{
    using System.Collections.Generic;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class NewsListViewModel : IMapFrom<NewsPost>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<NewsListItemViewModel> News { get; set; }
    }
}
