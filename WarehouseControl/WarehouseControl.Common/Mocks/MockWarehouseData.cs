using WarehouseControl.Common.Models;

namespace WarehouseControl.Common.Mocks
{
    public static class MockWarehouseData
    {
        public static List<Product> Products =>
            new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Description = "14-inch laptop", Size = "Medium" },
                new Product { ProductId = 2, Name = "Smartphone", Description = "5.5-inch smartphone", Size = "Small" },
                new Product
                {
                    ProductId = 3, Name = "Refrigerator", Description = "Two-door refrigerator", Size = "Large"
                }
            };


        public static List<Warehouse> Warehouses =>
            new List<Warehouse>
            {
                new Warehouse
                    { Id = 1, Name = "Main Warehouse", CityId = 101, CountryId = "US", WarehouseIdentifier = 1 },
                new Warehouse
                    { Id = 2, Name = "Backup Warehouse", CityId = 102, CountryId = "US", WarehouseIdentifier = 2 }
            };


        public static List<Rack> Racks =>
            new List<Rack>
            {
                new Rack { Id = 1, Sector = "A", Row = 1, Warehouse = Warehouses[0] },
                new Rack { Id = 2, Sector = "B", Row = 2, Warehouse = Warehouses[0] },
                new Rack { Id = 3, Sector = "C", Row = 3, Warehouse = Warehouses[1] }
            };


        public static List<Stock> GetMockStocks =>
            new List<Stock>
            {
                new Stock { StockId = 1, Rack = Racks[0], Type = Products[0], Count = 50 },
                new Stock { StockId = 2, Rack = Racks[1], Type = Products[1], Count = 100 },
                new Stock { StockId = 3, Rack = Racks[2], Type = Products[2], Count = 10 }
            };
    }
}