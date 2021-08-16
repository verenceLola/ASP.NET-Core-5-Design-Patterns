using System;

namespace Web.DTO
{
    public class ProductDetails
    {
        public ProductDetails(int id, string name, int quantityInStock)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            QuantityInStock = quantityInStock;
        }

        public int Id { get; }
        public string Name { get; }
        public int QuantityInStock { get; }
    }
}
