using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverviewApplication.Models
{
    public class HealthContent
    {
        public string status { get; set; }
        public string service { get; set; }
        public Uri HealthEndpoint { get; set; }
        public int Id { get; set; }
        public HealthItem[] results { get; set; }
    }
}
