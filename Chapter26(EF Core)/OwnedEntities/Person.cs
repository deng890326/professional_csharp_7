using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnedEntities
{
    public class Person
    {
        public int PersonId { get; }
        public string Name { get; set; }
        public Address PrivateAddress { get; set; }
        public Address CompanyAddress { get; set; }
    }
}
