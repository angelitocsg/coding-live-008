using System;
using System.Collections.Generic;
using System.Linq;
using ApiMapped.Domain.Entities;

namespace ApiMapped.ViewModels
{
    public class ProductViewModel
    {
        private ProductViewModel() { /* for AutoMapper */ }

        public ProductViewModel(Guid id, string productName, string category, decimal price, string codeBar, decimal upperPrice, decimal lowerPrice)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            Price = price;
            CodeBar = codeBar;
            UpperPrice = upperPrice;
            LowerPrice = lowerPrice;
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal UpperPrice { get; set; }
        public decimal LowerPrice { get; set; }
        public string CodeBar { get; set; }

        public static implicit operator ProductViewModel(Product product)
        {
            return new ProductViewModel(
                id: product.Id,
                productName: product.ProductName,
                category: product.Category.CategoryName,
                price: product.Prices.Last().Price,
                codeBar: product.CodeBar,
                upperPrice: product.UpperPrice.Price,
                lowerPrice: product.LowerPrice.Price
            );
        }

        public static implicit operator Product(ProductViewModel product)
        {
            return new Product(
                id: product.Id,
                productName: product.ProductName,
                category: new Category(product.Category),
                prices: new List<PriceHistory> { new PriceHistory(product.Price) },
                codeBar: product.CodeBar
            );
        }
    }
}