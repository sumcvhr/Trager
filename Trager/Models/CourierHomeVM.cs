using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class CourierHomeVM
    {
        public List<Il> Il {get; set;}
        public List<Ilce> Ilce {get; set;}
        public Courier courier { get; set; }
        public User user { get; set; }
        public CourierToken CourierToken { get; set; }
        public List<CourierComment>  comment { get; set; }
        public List<CourierLocation> Konum { get; set; }
        //public List<Chat> Chat { get; set; }
    }
}
