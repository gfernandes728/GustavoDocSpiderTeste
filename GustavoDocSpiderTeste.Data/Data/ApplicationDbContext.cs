using GustavoDocSpiderTeste.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace GustavoDocSpiderTeste.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<DocumentModel> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentModel>().HasIndex(d => d.Title).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}