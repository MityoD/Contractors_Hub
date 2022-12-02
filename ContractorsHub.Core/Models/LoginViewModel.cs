using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Core.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
