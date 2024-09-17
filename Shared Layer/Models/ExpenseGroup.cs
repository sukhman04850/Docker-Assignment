using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared_Layer.Models
{
    public class ExpenseGroup
    {
        
        
        public Guid GroupId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public List<Users> Users { get; set; } = new List<Users>();
        [JsonIgnore]
        public List<Expenses> Expenses { get; set; } = new List<Expenses>();
    }

    
}
