namespace TFNG.Web.ViewModels.Awards
{
    using System;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class AwardsListItemViewModel : IMapFrom<Award>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Place { get; set; }
    }
}
