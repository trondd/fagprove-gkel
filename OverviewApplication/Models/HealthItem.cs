using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverviewApplication.Models
{
    public class HealthItem
    {
        //Datamodel for the result of a single healthcheck from a service
        public string name { get; set; }

        public string status { get; set; }

        public string description { get; set; }
    }
}
