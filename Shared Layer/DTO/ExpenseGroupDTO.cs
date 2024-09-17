using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.DTO
{
    public class ExpenseGroupDTO
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
