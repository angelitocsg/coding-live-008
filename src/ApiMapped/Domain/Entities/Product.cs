using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMapped.Domain.Entities
{
    public class Product
    {
        private Product() { /* for AutoMapper */ }

        public Product(Guid id, string productName, Category category, ICollection<PriceHistory> prices, string codeBar)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            Prices = prices;
            UpperPrice = prices.Where(x => x.Price == prices.Max(y => y.Price)).FirstOrDefault();
            LowerPrice = prices.Where(x => x.Price == prices.Min(y => y.Price)).FirstOrDefault();
            CodeBar = codeBar;
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public ICollection<PriceHistory> Prices { get; set; }
        public PriceHistory UpperPrice { get; set; }
        public PriceHistory LowerPrice { get; set; }
        public string CodeBar { get; set; }
    }
}
