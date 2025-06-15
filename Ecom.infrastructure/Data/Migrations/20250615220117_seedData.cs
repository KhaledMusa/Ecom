using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 15, 22, 1, 17, 573, DateTimeKind.Utc).AddTicks(661), new DateTime(2025, 6, 15, 22, 1, 17, 573, DateTimeKind.Utc).AddTicks(662) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 15, 21, 56, 23, 503, DateTimeKind.Utc).AddTicks(505), new DateTime(2025, 6, 15, 21, 56, 23, 503, DateTimeKind.Utc).AddTicks(507) });
        }
    }
}
