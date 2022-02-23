namespace Fiorella.Models.ViewModel
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Count { get; set; } = 1;
        
    }
}