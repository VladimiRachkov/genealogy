using Microsoft.EntityFrameworkCore;

namespace Genealogy.Models {
    public class GenealogyContext : DbContext {
        public GenealogyContext (DbContextOptions<GenealogyContext> options) : base (options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cemetery> Cemeteries { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) { }
    }
}