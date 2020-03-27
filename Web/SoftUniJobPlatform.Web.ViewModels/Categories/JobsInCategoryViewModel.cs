namespace SoftUniJobPlatform.Web.ViewModels.Categories
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class JobsInCategoryViewModel : IMapFrom<Job>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                    ? content.Substring(0, 300) + "..."
                    : content;
            }
        }

        public string UserUserName { get; set; }

        public int JobsCount { get; set; }
    }
}
