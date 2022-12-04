using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Models.Contractor
{
    public class ContractorViewModel
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        //public string Trades { get; set; } = null!;

        public string Rating { get; set; } = null!;
    }
}
