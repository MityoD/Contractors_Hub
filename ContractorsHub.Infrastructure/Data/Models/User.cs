using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public bool IsContractor { get; set; }

        //public IEnumerable<> MyProperty { get; set; }
    }
}
