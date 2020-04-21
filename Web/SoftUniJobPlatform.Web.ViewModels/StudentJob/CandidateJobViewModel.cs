namespace SoftUniJobPlatform.Web.ViewModels.StudentJob
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CandidateJobViewModel : IMapFrom<Job>
    {
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<StudentJob> Candidates { get; set; }

    }
}