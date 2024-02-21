﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repositories.EFCore;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 75m,
                            Title = "True Way Chatapter 1: Being Darksiders"
                        },
                        new
                        {
                            Id = 2,
                            Price = 125m,
                            Title = "True Way Chatapter 2: Being a Thief"
                        },
                        new
                        {
                            Id = 3,
                            Price = 50m,
                            Title = "True Way Chatapter 3: Being a Wolf"
                        },
                        new
                        {
                            Id = 4,
                            Price = 35m,
                            Title = "True Way Chatapter 4: Being a WarLord"
                        },
                        new
                        {
                            Id = 5,
                            Price = 15m,
                            Title = "True Way Chatapter 5: Being a TimeLord"
                        },
                        new
                        {
                            Id = 6,
                            Price = 15m,
                            Title = "True Way Chatapter 6: Being a AncientLord"
                        },
                        new
                        {
                            Id = 7,
                            Price = 15m,
                            Title = "True Way Chatapter 7: Being a ShadowLord"
                        },
                        new
                        {
                            Id = 8,
                            Price = 15m,
                            Title = "True Way Chatapter 8: Champion of the Darkness"
                        },
                        new
                        {
                            Id = 9,
                            Price = 15m,
                            Title = "True Way Chatapter 9: Absolute Power"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
