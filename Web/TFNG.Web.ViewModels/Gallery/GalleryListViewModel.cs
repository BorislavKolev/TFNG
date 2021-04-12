namespace TFNG.Web.ViewModels.Gallery
{
    using System.Collections.Generic;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class GalleryListViewModel : IMapFrom<Picture>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<GalleryListItemViewModel> Pictures { get; set; }
    }
}
