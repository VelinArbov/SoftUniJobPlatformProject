// ReSharper disable VirtualMemberCallInConstructor

using SoftUniJobPlatform.Data.Models.Enum;

namespace SoftUniJobPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using SoftUniJobPlatform.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Jobs = new HashSet<Job>();
            this.Courses = new HashSet<StudentCourse>();
            this.StudentJobs = new HashSet<StudentJob>();
            this.Categories = new HashSet<Category>();
            this.UsersSkills = new HashSet<UsersSkill>();
        }

        public string Tittle { get; set; }

        public string RegistrationNumber { get; set; }

        public UserType Type { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public double Score { get; set; }

        public GenderType Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<StudentCourse> Courses { get; set; }

        public virtual ICollection<StudentJob> StudentJobs { get; set; }

        public virtual ICollection<UsersSkill> UsersSkills { get; set; }
    }
}
