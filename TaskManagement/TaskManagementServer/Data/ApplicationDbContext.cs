using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Models;

namespace TaskManagementServer.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<TeamRequest> TeamRequests { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(config.GetSection("ConnectionStrings")
            .GetSection("DefaultConnection").Value);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
