using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle Villa Real", new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7203), new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7164), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle Villa", new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7209), new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7207), "", 40, "Premiun Vista a las Piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
