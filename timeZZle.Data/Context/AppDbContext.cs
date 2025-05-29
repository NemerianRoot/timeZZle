using Microsoft.EntityFrameworkCore;
using timeZZle.Domain.Entities;

namespace timeZZle.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Box> Boxes { get; set; }
    public DbSet<Clock> Clocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        base.OnConfiguring(opt);
        opt.UseInMemoryDatabase("timeZZle.db");
        //opt.UseSqlServer("server=localhost\\SQLEXPRESS;database=timezzle;user=app;password=appPassword");
        // Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
        
    }
}