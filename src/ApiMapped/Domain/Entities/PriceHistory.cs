using System;

namespace ApiMapped.Domain.Entities
{
    public class PriceHistory
    {
        public PriceHistory(decimal price)
        {
            Price = price;
            ValidFrom = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public decimal Price { get; set; }
    }
}
