namespace TFNG.Web.ViewModels.Dances
{
    using Microsoft.AspNetCore.Http;

    public class DancesCreateInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string FolkloreArea { get; set; }

        public IFormFile Picture { get; set; }
    }
}
