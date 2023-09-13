using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 25, 18, 489, DateTimeKind.Local).AddTicks(4757), new DateTime(2023, 9, 11, 10, 25, 18, 489, DateTimeKind.Local).AddTicks(4716) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 11, 10, 25, 18, 489, DateTimeKind.Local).AddTicks(4762), new DateTime(2023, 9, 11, 10, 25, 18, 489, DateTimeKind.Local).AddTicks(4761) });
        }
    }
}
