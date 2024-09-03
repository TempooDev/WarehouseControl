namespace WarehouseControl.Common.Models;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
    public string CountryId { get; set; }
    public int WarehouseIdentifier { get; set; }
    public string WarehouseCode => CountryId + "-" + CityId+"-"+WarehouseIdentifier;
}
