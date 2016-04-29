using System.Data.Entity;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Character> Products { get; set; }
    }
}