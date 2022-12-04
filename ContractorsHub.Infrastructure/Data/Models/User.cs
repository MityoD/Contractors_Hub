using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public bool IsContractor { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; } = null;

        [StringLength(50)]
        public string? LastName { get; set; } = null;
    }
}
