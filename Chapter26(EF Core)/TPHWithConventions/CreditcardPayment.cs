using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHWithConventions
{
    public class CreditcardPayment : Payment
    {
        public string CreditcardNumber { get; set; } = string.Empty;


        public CreditcardPayment(string name) : base(name)
        {
        }
    }
}
