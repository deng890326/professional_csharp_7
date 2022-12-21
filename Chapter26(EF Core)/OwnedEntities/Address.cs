using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnedEntities
{
    public class Address
    {
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public Location Location { get; set; }
    }
}
