namespace WarehouseControl.Common.Models;

public class Rack
{
    public int Id { get; set; }
    public string Sector { get; set; }
    public int Row { get; set; }
    public Warehouse Warehouse { get; set; }
}