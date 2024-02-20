using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            
            builder.HasData(
                new Book { Id = 1, Title = "True Way Chatapter 1: Being Darksiders", Price = 75 },
                new Book { Id = 2, Title = "True Way Chatapter 2: Being a Thief", Price = 125 },
                new Book { Id = 3, Title = "True Way Chatapter 3: Being a Wolf", Price = 50 },
                new Book { Id = 4, Title = "True Way Chatapter 4: Being a WarLord", Price = 35 },
                new Book { Id = 5, Title = "True Way Chatapter 5: Being a TimeLord", Price = 15 },
                new Book { Id = 6, Title = "True Way Chatapter 6: Being a AncientLord", Price = 15 },
                new Book { Id = 7, Title = "True Way Chatapter 7: Being a ShadowLord", Price = 15 },
                new Book { Id = 8, Title = "True Way Chatapter 8: Champion of the Darkness", Price = 15 },
                new Book { Id = 9, Title = "True Way Chatapter 9: Absolute Power", Price = 15 }
                );
        }
    }
}
