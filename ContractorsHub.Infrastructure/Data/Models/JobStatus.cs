using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class JobStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
