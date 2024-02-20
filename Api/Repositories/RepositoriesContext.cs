using Api.Models;
using Api.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class RespositoryContext : DbContext
    {
        public RespositoryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }


    }
}
