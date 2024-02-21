using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class startPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 75m, "True Way Chatapter 1: Being Darksiders" },
                    { 2, 125m, "True Way Chatapter 2: Being a Thief" },
                    { 3, 50m, "True Way Chatapter 3: Being a Wolf" },
                    { 4, 35m, "True Way Chatapter 4: Being a WarLord" },
                    { 5, 15m, "True Way Chatapter 5: Being a TimeLord" },
                    { 6, 15m, "True Way Chatapter 6: Being a AncientLord" },
                    { 7, 15m, "True Way Chatapter 7: Being a ShadowLord" },
                    { 8, 15m, "True Way Chatapter 8: Champion of the Darkness" },
                    { 9, 15m, "True Way Chatapter 9: Absolute Power" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
