using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public bool IsContractor { get; set; }
    }
}
