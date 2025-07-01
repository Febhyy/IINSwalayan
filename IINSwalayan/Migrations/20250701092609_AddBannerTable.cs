using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IINSwalayan.Migrations
{
    /// <inheritdoc />
    public partial class AddBannerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    TextColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "BackgroundColor", "ButtonText", "CreatedAt", "Description", "EndDate", "ImageUrl", "IsActive", "LinkUrl", "Order", "StartDate", "TextColor", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "#FF69B4", "KLIK DI SINI", new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6604), "Harga Spesial untuk Semua GIFTCARD - Periode 17-23 Juni 2025", new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x300/FF69B4/FFFFFF?text=MIDMONTH+MADNESS", true, "/Home/Products", 1, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "#FFFFFF", "MIDMONTH MADNESS", new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6605) },
                    { 2, "#1E3A8A", "KLIK DI SINI", new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6612), "Exclusive Collectible Figurine - Hanya 10 STAMP + Rp 29.900", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x300/1E3A8A/FFFFFF?text=INDONESIA+JUARA", true, "/Home/Products", 2, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "#FFFFFF", "INDONESIA JUARA", new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6613) }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6544), new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6545) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6550), new DateTime(2025, 7, 1, 16, 26, 4, 744, DateTimeKind.Local).AddTicks(6551) });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_EndDate",
                table: "Banners",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_IsActive",
                table: "Banners",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Order",
                table: "Banners",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_StartDate",
                table: "Banners",
                column: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 15, 21, 47, 950, DateTimeKind.Local).AddTicks(9028), new DateTime(2025, 7, 1, 15, 21, 47, 950, DateTimeKind.Local).AddTicks(9042) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 15, 21, 47, 950, DateTimeKind.Local).AddTicks(9052), new DateTime(2025, 7, 1, 15, 21, 47, 950, DateTimeKind.Local).AddTicks(9053) });
        }
    }
}
