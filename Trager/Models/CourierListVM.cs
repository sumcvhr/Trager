using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class CourierListVM
    {
        public List<Courier> couriers { get; set; }
        public List<CourierLocation> CourierLocations { get; set; }
        public List<Il> Ils { get; set; }
        public List<Ilce> Ilces { get; set; }
       
    }
}
