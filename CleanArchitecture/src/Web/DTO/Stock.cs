namespace Web.DTO
{
    public class AddStocksCommand
    {
        public int Amount { get; set; }
    }

    public class RemoveStocksCommand
    {
        public int Amount { get; set; }
    }
    public class StockLevel
    {
        public StockLevel(int quantityInStock)
        {
            QuantityInStock = quantityInStock;
        }
        public int QuantityInStock { get; set; }
    }
}
