namespace TFNG.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TFNG.Data.Common.Models;

    public class Award : BaseDeletableModel<int>
    {
        public Award()
        {
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LatinName { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
