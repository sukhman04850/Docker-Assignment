using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.DTO
{
    public class IncomingExpense
    {
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public List<Guid> Payers { get; set; } = new List<Guid>();

    }
}
