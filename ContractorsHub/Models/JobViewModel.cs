using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models
{
    public class JobViewModel
    {      
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;//Address

        public string OwnerName { get; set; } = null!;

        public DateTime StartDate { get; set; }

    }
}
