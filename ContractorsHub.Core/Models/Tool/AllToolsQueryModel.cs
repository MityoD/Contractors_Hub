using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Models.Tool
{
    public class AllToolsQueryModel
    {
        public const int ToolsPerPage = 3;

        public string? Category { get; set; }

        public string? SearchTerm { get; set; }

        public ToolSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalToolsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<ToolViewModel> Tools { get; set; } = Enumerable.Empty<ToolViewModel>();
    }
}
