namespace TFNG.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class NewsCreateInputModel
    {
        [Required(ErrorMessage = "Please enter news post title.")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter news post content")]
        public string Content { get; set; }

        public string Author { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }
    }
}
