namespace TFNG.Web.ViewModels.Dances
{
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class DancesListItemViewModel : IMapFrom<Dance>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public string Summary
        {
            get
            {
                var description = this.Description;
                return description.Length > 480 ? description.Substring(0, 480) + "..." : description;
            }
        }

        public string ImageUrl { get; set; }

        public string FolkloreArea { get; set; }

        public string DanceUrl => $"/Dances/{this.LatinName.Replace(' ', '-')}";
    }
}
