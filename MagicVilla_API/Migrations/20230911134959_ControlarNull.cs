using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 49, 58, 984, DateTimeKind.Local).AddTicks(3546), new DateTime(2023, 9, 11, 10, 49, 58, 984, DateTimeKind.Local).AddTicks(3494) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 49, 58, 984, DateTimeKind.Local).AddTicks(3553), new DateTime(2023, 9, 11, 10, 49, 58, 984, DateTimeKind.Local).AddTicks(3551) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
