using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Models
{
    public class ExpenseShare
    {
        
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public Guid ExpenseId { get; set; }
        public Expenses? Expense { get; set; }
    }
}
