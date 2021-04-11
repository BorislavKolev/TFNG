namespace TFNG.Web.ViewModels.Dances
{
    using Microsoft.AspNetCore.Http;
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class DanceEditViewModel : IMapFrom<Dance>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Picture { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public string FolkloreArea { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}
