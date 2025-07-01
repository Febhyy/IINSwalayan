using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IINSwalayan.Migrations
{
    /// <inheritdoc />
    public partial class Migrasibaru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 50, 15, 344, DateTimeKind.Local).AddTicks(1825), new DateTime(2025, 6, 10, 0, 50, 15, 344, DateTimeKind.Local).AddTicks(1835) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 50, 15, 344, DateTimeKind.Local).AddTicks(1841), new DateTime(2025, 6, 10, 0, 50, 15, 344, DateTimeKind.Local).AddTicks(1841) });
        }
    }
}
