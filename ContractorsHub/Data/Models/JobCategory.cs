﻿using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Data.Models
{
    public class JobCategory
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
