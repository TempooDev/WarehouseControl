namespace WarehouseControl.Common.Models;

public class Stock
{
    public int StockId { get; set; }
    public Rack Rack { get; set; }
    public Product Type { get; set; }
    public int Count { get; set; }
}