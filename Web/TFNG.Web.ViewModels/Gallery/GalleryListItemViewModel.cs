namespace TFNG.Web.ViewModels.Gallery
{
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class GalleryListItemViewModel : IMapFrom<Picture>
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }
    }
}
