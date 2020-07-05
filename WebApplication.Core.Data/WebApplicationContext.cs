using Microsoft.EntityFrameworkCore;
using WebApplication.Core.Domain;

namespace WebApplication.Core.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext()
        {
        }

        public WebApplicationContext(DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
