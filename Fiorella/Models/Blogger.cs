using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class Blogger
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Image { get; set; }
        [Required]
        public string BloggerTitle { get; set; }
        [Required]
        public string BloggerSubTitle { get; set; }
    }
}