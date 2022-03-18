using DiscountCodes.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class DiscountDB : DbContext
{
    public string DbPath { get; }
    public DiscountDB(DbContextOptions options) : base(options) {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "discounts.db");
    }
    public DbSet<DiscountCode> DiscountCodes { get; set; }
    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>().HasData(
            new Store { Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), ShortName = "Cheese" });
    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

}