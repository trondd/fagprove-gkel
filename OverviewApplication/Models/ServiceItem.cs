using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverviewApplication.Models
{
    public class ServiceItem
    {
        //Datamodel for the "summary" of health checks from a single service
        public string serviceName { get; set; }
        public string status { get; set; }
        public Uri endpoint { get; set; }
        public int id { get; set; }
    }
}
