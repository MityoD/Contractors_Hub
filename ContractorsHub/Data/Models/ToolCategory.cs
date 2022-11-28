using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Data.Models
{
    public class ToolCategory
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<Tool> Tools { get; set; } = new List<Tool>();
    }
}
