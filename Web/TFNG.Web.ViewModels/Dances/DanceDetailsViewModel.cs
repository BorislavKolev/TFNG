namespace TFNG.Web.ViewModels.Dances
{
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class DanceDetailsViewModel : IMapFrom<Dance>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string FolkloreArea { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
