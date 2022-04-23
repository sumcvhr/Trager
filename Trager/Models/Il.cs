using System;
using System.Collections.Generic;

#nullable disable

namespace Trager.Models
{
    public partial class Il
    {
        public Il()
        {
            Ilces = new HashSet<Ilce>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public int PlakaKodu { get; set; }

        public virtual ICollection<Ilce> Ilces { get; set; }
    }
}
