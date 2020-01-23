using Microsoft.EntityFrameworkCore;

namespace Svc.Implementation.Model
{
    public sealed class ServiceDbContext : DbContext
    {
        public DbSet<LongRunningTask> Tasks { get; set; }
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
            : base(options)
        {

        }

        public ServiceDbContext(DbContextOptions o) : base(o)
        {

        }


        public ServiceDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=example-sql,1433;Database=ExampleDb;user=sa;password=555331qQ!;Trusted_Connection=False;");
            base.OnConfiguring(builder);
        }
    }
}
