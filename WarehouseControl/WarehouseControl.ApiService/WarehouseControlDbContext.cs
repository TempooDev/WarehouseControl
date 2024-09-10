using Microsoft.EntityFrameworkCore;
using WarehouseControl.Common.Models;

public class WarehouseControlDbContext : DbContext{

    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> warehouses{ get; set; }

    public DbSet<Stock> Stocks{ get; set; }
    public DbSet<Rack> Racks{ get; set; }

    
}