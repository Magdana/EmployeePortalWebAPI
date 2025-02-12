using EmployeePortalWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWebAPI.DAL;

public class EmpolyeePortalDbContext : DbContext
{
    public EmpolyeePortalDbContext(DbContextOptions<EmpolyeePortalDbContext> options) : base(options) { }
    public DbSet<EmployeeEntity>? Employees { get; set; }
    public DbSet<CompanyEntity>? Companies { get; set; }
    public DbSet<UserEntity>? Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeEntity>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.SetNull); 
    }

}
