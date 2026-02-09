using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Electronics", "A high-performance laptop for work and gaming.", "Laptop 1", 999.99m },
                    { 2, "Electronics", "A sleek smartphone with a powerful camera.", "Smartphone 2", 699.99m },
                    { 3, "Audio", "Noise-cancelling headphones for immersive sound.", "Headphones 3", 199.99m },
                    { 4, "Home Appliances", "Brew the perfect cup of coffee every morning.", "Coffee Maker 4", 49.99m },
                    { 5, "Footwear", "Comfortable running shoes for all terrains.", "Running Shoes 5", 89.99m },
                    { 6, "Accessories", "A durable backpack for travel and daily use.", "Backpack 6", 59.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
