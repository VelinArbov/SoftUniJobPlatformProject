namespace SoftUniJobPlatform.Web.ViewModels.Student
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class StudentDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Tittle { get; set; }

        public string RegistrationNumber { get; set; }

        public string ImageUrl { get; set; }


        public string Location { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public double Score { get; set; }

        public bool IsAdmin { get; set; }

        public GenderType Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
