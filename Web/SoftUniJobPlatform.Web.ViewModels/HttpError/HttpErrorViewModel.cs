namespace SoftUniJobPlatform.Web.ViewModels.HttpError
{
    public class HttpErrorViewModel
    {
        public string Content { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
