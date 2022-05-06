using Microsoft.EntityFrameworkCore;

namespace p9_c00299690_c00299553.Data;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

    public DbSet<Models.Inventory> Inventories { get; set; }
}