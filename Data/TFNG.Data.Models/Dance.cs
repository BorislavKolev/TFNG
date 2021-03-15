namespace TFNG.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TFNG.Data.Common.Models;
    using TFNG.Data.Models;

    public class Dance : BaseDeletableModel<int>
    {
        public Dance()
        {
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LatinName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        [Required]
        public string FolkloreArea { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
