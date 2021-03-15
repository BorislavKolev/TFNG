namespace TFNG.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TFNG.Data.Common.Models;

    public class NewsPostPicture : BaseDeletableModel<int>
    {
        [Required]
        public int NewsPostId { get; set; }

        public virtual NewsPost NewsPost { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
