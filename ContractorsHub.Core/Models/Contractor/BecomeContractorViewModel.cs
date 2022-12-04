using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Core.Models.Contractor
{
    public class BecomeContractorViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 7)]  
        [Display(Name = "Phone number")]
        [Phone] //validate phone number?
        public string PhoneNumber { get; set; } = null!;

    }
}
