using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsNewAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method
        | AttributeTargets.Constructor, AllowMultiple = true, Inherited = false)]
    public class LastModifiedAttribute : Attribute
    {
        public DateTime DateModified { get; }
        public string Changes { get; }
        public string? Issues { get; set; }

        public LastModifiedAttribute(string dateModified, string changes)
        {
            DateModified = Convert.ToDateTime(dateModified);
            Changes = changes;
        }
    }
}
