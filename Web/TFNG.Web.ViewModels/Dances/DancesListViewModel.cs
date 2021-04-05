namespace TFNG.Web.ViewModels.Dances
{
    using System.Collections.Generic;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class DancesListViewModel : IMapFrom<Dance>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<DancesListItemViewModel> Dances { get; set; }
    }
}
