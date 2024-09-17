using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared_Layer.Models
{
    public class Expenses
    {
        
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Users? User { get; set; }
        
        public Guid ExpenseGroupId { get; set; }
        [JsonIgnore]
        public ExpenseGroup? ExpenseGroup { get; set; }
        [JsonIgnore]
        public List<ExpenseShare> ExpenseShares { get; set; } = new List<ExpenseShare>();
    }
}
