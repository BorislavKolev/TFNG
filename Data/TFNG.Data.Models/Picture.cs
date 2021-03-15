namespace TFNG.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TFNG.Data.Common.Models;
    using TFNG.Data.Models;

    public class Picture : BaseDeletableModel<int>
    {
        public Picture()
        {
        }

        [Required]
        public string Type { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
