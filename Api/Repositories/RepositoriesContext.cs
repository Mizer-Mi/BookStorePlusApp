using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class RespositoryContext : DbContext
    {
        public RespositoryContext(DbContextOptions options) : base(options)
        {

        }



        public DbSet<Book> Books { get; set; }
    }
}
