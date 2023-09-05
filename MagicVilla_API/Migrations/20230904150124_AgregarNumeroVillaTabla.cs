using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "numerovillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numerovillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_numerovillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 4, 12, 1, 24, 62, DateTimeKind.Local).AddTicks(8981), new DateTime(2023, 9, 4, 12, 1, 24, 62, DateTimeKind.Local).AddTicks(8943) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 4, 12, 1, 24, 62, DateTimeKind.Local).AddTicks(8987), new DateTime(2023, 9, 4, 12, 1, 24, 62, DateTimeKind.Local).AddTicks(8985) });

            migrationBuilder.CreateIndex(
                name: "IX_numerovillas_VillaId",
                table: "numerovillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "numerovillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7203), new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7164) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7209), new DateTime(2023, 8, 30, 8, 26, 37, 589, DateTimeKind.Local).AddTicks(7207) });
        }
    }
}
