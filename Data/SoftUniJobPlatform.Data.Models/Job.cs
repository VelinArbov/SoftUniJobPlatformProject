﻿namespace SoftUniJobPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Common.Models;

    public class Job : BaseDeletableModel<int>
    {
        public Job()
        {
            this.Candidates = new HashSet<StudentJob>();
        }

        public ApplicationUser ApplicationUser { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<StudentJob> Candidates { get; set; }

    }
}
