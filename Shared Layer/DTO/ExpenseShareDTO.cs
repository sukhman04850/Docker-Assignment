using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.DTO
{
    public class ExpenseShareDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public Guid ExpenseId { get; set; }
        public Expenses? Expense { get; set; }
    }
}
