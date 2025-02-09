using EmployeePortalWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWebAPI.DAL;

public class EmpolyeePortalDbContext : DbContext
{
    public EmpolyeePortalDbContext(DbContextOptions<EmpolyeePortalDbContext> options) : base(options) { }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
}
