using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class BioHeader
    {
        public int Id { get; set; }
        [Required]
        public string Logo { get; set; }
        [StringLength(100)]
        public string FacebookUrl { get; set; }
        [StringLength(100)]
        public string LinkedinUrl { get; set; }
    }
}