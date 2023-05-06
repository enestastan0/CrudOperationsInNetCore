using Microsoft.EntityFrameworkCore;

namespace CrudOperationsInNetCore.Models
{
    public class GorselprgContext : DbContext
    {
        public GorselprgContext(DbContextOptions<GorselprgContext> options) : base(options)
        {
            
        }
        public DbSet<Gorselprg> Gorselprg { get; set;}
    }
}
