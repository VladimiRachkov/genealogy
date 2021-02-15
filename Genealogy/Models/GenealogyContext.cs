using Microsoft.EntityFrameworkCore;

namespace Genealogy.Models
{
    public class GenealogyContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cemetery> Cemeteries { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PersonGroup> PersonGroups { get; set; }
        public DbSet<BusinessObject> BusinessObjects { get; set; }
        public DbSet<Metatype> Metatypes { get; set; }
        public DbSet<ObjectRelation> ObjectRelations { get; set; }
        public DbSet<LinkRelation> LinkRelations { get; set; }
        public GenealogyContext(DbContextOptions<GenealogyContext> options) : base(options) { }
    }
}