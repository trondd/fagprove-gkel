using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OverviewApplication.Models
{
    public class Endpoint
    {
        //Datamodel for the URL pointing to the services
        public string Name { get; set; }
        public Uri Uri { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
