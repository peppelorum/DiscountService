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
    public DbSet<DiscountUse> DiscountUses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Student>().HasNoKey();
        // modelBuilder.Entity<DiscountCodeJob>().OwnsOne(x => x.Codes);
        modelBuilder.Entity<DiscountCodeJob>()
        .Property(e => e.Codes)
        .HasConversion(
        v => JsonConvert.SerializeObject(v),
        v => JsonConvert.DeserializeObject<List<string>>(v));
    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

}