using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OverviewApplication.Models;

namespace OverviewApplication.Services
{
    public interface IRepository
    {
        Task<List<HealthResultContent>> GetAll(List<Endpoint> endpoints);
    }
}
