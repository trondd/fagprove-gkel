using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverviewApplication.Models
{
    public class ServiceItem
    {
        public string serviceName { get; set; }
        public string status { get; set; }
        public Uri endpoint { get; set; }
        public int id { get; set; }
    }
}
