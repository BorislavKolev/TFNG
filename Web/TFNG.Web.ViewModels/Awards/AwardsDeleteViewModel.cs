namespace TFNG.Web.ViewModels.Awards
{
    using System;

    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class AwardsDeleteViewModel : IMapFrom<Award>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LatinName { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Place { get; set; }
    }
}
