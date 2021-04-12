namespace TFNG.Web.ViewModels.Gallery
{
    using Microsoft.AspNetCore.Http;
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class PictureCreateInputModel : IMapFrom<Picture>
    {
        public IFormFile Picture { get; set; }

        public string Type { get; set; }
    }
}
