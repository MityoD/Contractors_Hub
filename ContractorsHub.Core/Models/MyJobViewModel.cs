namespace ContractorsHub.Core.Models
{
    public class MyJobViewModel
    {      
        public int Id { get; set; }

        public string OwnerId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsTaken { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsApproved { get; set; } = false;

        public string Status { get; set; } = null!;

        public string? ContractorId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
