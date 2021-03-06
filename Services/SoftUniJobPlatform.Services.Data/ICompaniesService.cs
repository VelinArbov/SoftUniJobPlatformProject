﻿namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetCompanyAsync<T>(string id);

    }
}
