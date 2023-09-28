using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Service.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_BaseCategoryId",
                        column: x => x.BaseCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "BaseCategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, null, "Yemek" },
                    { 2, null, "Elektronik" },
                    { 8, null, "Temizlik" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryId", "ImageUrl", "StockQuantity" },
                values: new object[] { 7, "assets/images/14.jpg", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "ImageUrl", "StockQuantity" },
                values: new object[] { 7, "assets/images/12.jpg", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CategoryId", "ImageUrl", "StockQuantity" },
                values: new object[] { 6, "assets/images/11.jpg", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "ImageUrl", "StockQuantity" },
                values: new object[] { 3, "assets/images/13.jpg", 10 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "BaseCategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 3, 1, "Ana Yemek" },
                    { 4, 2, "Telefon" },
                    { 6, 1, "Tatlı" },
                    { 7, 1, "Aparatif" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { 5, 8, "Çevre dostu tuvalet kağıdı.", "assets/images/Tuvalet Kağıdı.webp", "Solo Bambu Katkılı 40'lı Tuvalet Kağıdı", 19.0, 10 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "BaseCategoryId", "CategoryName" },
                values: new object[] { 5, 4, "Android Telefon" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 6, 5, "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)", "assets/images/Redmi Note 12 Pro.webp", "Xiaomi Redmi Note 12 Pro", 1900.0, 10 },
                    { 7, 5, "Samsung Galaxy A04E 4 GB 128 GB (Samsung Türkiye Garantili)", "assets/images/Samsung Galaxy A04E.jpg", "Samsung Galaxy A04E", 1700.0, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BaseCategoryId",
                table: "Categories",
                column: "BaseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryName", "ImageUrl", "StockQuantity" },
                values: new object[] { "Appetizer", "https://dotnetmastery.blob.core.windows.net/mango/14.jpg", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryName", "ImageUrl", "StockQuantity" },
                values: new object[] { "Appetizer", "https://dotnetmastery.blob.core.windows.net/mango/12.jpg", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CategoryName", "ImageUrl", "StockQuantity" },
                values: new object[] { "Dessert", "https://dotnetmastery.blob.core.windows.net/mango/11.jpg", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryName", "ImageUrl", "StockQuantity" },
                values: new object[] { "Entree", "https://dotnetmastery.blob.core.windows.net/mango/13.jpg", 0 });
        }
    }
}
