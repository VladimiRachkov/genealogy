using Microsoft.EntityFrameworkCore;

namespace Genealogy.Models
{
    public class GenealogyContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cemetery> Cemeteries { get; set; }
        public GenealogyContext(DbContextOptions<GenealogyContext> options) : base(options) { }

    }
}