using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNullabless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 36, 19, 378, DateTimeKind.Local).AddTicks(306), new DateTime(2023, 9, 11, 10, 36, 19, 378, DateTimeKind.Local).AddTicks(264) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 36, 19, 378, DateTimeKind.Local).AddTicks(312), new DateTime(2023, 9, 11, 10, 36, 19, 378, DateTimeKind.Local).AddTicks(310) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 32, 28, 393, DateTimeKind.Local).AddTicks(8196), new DateTime(2023, 9, 11, 10, 32, 28, 393, DateTimeKind.Local).AddTicks(8153) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 32, 28, 393, DateTimeKind.Local).AddTicks(8202), new DateTime(2023, 9, 11, 10, 32, 28, 393, DateTimeKind.Local).AddTicks(8200) });
        }
    }
}
