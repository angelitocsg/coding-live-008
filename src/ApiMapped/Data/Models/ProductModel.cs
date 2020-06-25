using System;
using ApiMapped.Domain.Entities;

namespace ApiMapped.Data.Models
{
    public class ProductModel
    {
        public ProductModel(Guid id, string productName, Category category, PriceHistory[] prices, string ean13)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            Prices = prices;
            Ean13 = ean13;
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public PriceHistory[] Prices { get; set; }
        public string Ean13 { get; set; }

        public static implicit operator ProductModel(Product product)
        {
            return new ProductModel(
                id: product.Id,
                productName: product.ProductName,
                category: product.Category,
                prices: product.Prices,
                ean13: product.CodeBar
            );
        }

        public static implicit operator Product(ProductModel product)
        {
            return new Product(
                id: product.Id,
                productName: product.ProductName,
                category: product.Category,
                prices: product.Prices,
                codeBar: product.Ean13
            );
        }
    }
}
