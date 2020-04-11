using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SoftUniJobPlatform.Web.Areas.Identity.IdentityHostingStartup))]

namespace SoftUniJobPlatform.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}