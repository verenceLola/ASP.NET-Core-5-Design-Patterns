using System;

namespace Core.Exceptions
{
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException(int quantityInStock, int amountToRemove) : base(
            $"You cannot remove {amountToRemove} item(s) when there is only {quantityInStock} item(s) left."
        )
        {
            AmountToRemove = amountToRemove;
            QuantityInStock = quantityInStock;
        }
        public int QuantityInStock { get; }
        public int AmountToRemove { get; }
    }
}
