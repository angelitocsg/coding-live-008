using System;

namespace ApiMapped.Domain.Entities
{
    public class Product
    {
        private Product() { /* for AutoMapper */ }

        public Product(Guid id, string productName, Category category, PriceHistory[] prices, string codeBar)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            Prices = prices;
            CodeBar = codeBar;
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public PriceHistory[] Prices { get; set; }
        public string CodeBar { get; set; }
    }
}
