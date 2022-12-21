using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHWithConventions
{
    [PrimaryKey(nameof(PaymentId))]
    public abstract class Payment
    {
        public int PaymentId { get; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        public Payment(string name) => Name = name;
    }
}
