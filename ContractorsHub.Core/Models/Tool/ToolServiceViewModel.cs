using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Models.Tool
{
    public class ToolServiceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public decimal Price { get; set; }

    }
}
