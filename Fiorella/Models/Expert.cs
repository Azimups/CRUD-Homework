using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Models
{
    public class Expert
    {
        public int Id { get; set; }

        public string Image { get; set; }
        
        public string Name { get; set; }

        public string Job { get; set; }
        
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}