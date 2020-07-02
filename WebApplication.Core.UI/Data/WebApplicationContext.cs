using Microsoft.EntityFrameworkCore;
using WebApplication.Core.UI.Entities;

namespace WebApplication.Core.UI.Data
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
