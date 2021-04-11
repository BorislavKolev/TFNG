namespace TFNG.Web.ViewModels.News
{
    using System;
    using System.Text.RegularExpressions;
    using TFNG.Data.Models;
    using TFNG.Services.Mapping;

    public class NewsListItemViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LatinTitle { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public string Summary
        {
            get
            {
                var contentWithSingleSpaces = Regex.Replace(this.Content, @"\s+", " ");
                var startIndex = contentWithSingleSpaces.IndexOf('>');
                int endIndex = contentWithSingleSpaces.IndexOf('<', contentWithSingleSpaces.IndexOf('<') + 1);
                int length = endIndex - startIndex + 1;
                var content = contentWithSingleSpaces.Substring(startIndex + 1, length - 2);
                return content.Length > 380 ? content.Substring(0, 380) + "..." : content;
            }
        }

        public string NewsPostUrl => $"/News/{this.LatinTitle.Replace(' ', '-')}";
    }
}
