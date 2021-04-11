namespace TFNG.Web.ViewModels.Awards
{
    using System;

    using Microsoft.AspNetCore.Http;
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class AwardsEditViewModel : IMapFrom<Award>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Place { get; set; }

        public IFormFile Picture { get; set; }
    }
}
