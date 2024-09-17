using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared_Layer.Models
{
    public class Users
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        [JsonIgnore]
       public List<Expenses> Expenses { get; set; } = new List<Expenses>();
       
        [JsonIgnore]
        public List<ExpenseGroup> ExpenseGroups { get; set; } = new List<ExpenseGroup>();

    }
}
