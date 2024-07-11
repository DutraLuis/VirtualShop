using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                                 "Values('Caderno', 7.55, 'Caderno Credeal', 10, 'caderno1.jpg', 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                                 "Values('Lápis', 3.45, 'Lápis Faber Castell', 20, 'lapis1.jpg', 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                                 "Values('Clips', 5.33, 'Clips Tilibra', 50, 'clips1.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
