namespace TFNG.Web.ViewModels.Awards
{
    using System.Collections.Generic;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class AwardsListViewModel : IMapFrom<Award>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<AwardsListItemViewModel> Awards { get; set; }
    }
}
