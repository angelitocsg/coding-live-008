using System;

namespace ApiMapped.Domain.Entities
{
    public class PriceHistory
    {
        private PriceHistory() { /* EF */}

        public PriceHistory(decimal price)
        {
            Price = price;
            ValidFrom = DateTime.Now;
        }

        public PriceHistory(DateTime validFrom, decimal price)
        {
            ValidFrom = validFrom;
            Price = price;
        }

        public int Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public decimal Price { get; set; }
    }
}
