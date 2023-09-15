using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class usuariomigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_numerovillas_Villas_VillaId",
                table: "numerovillas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_numerovillas",
                table: "numerovillas");

            migrationBuilder.RenameTable(
                name: "numerovillas",
                newName: "NumeroVillas");

            migrationBuilder.RenameIndex(
                name: "IX_numerovillas_VillaId",
                table: "NumeroVillas",
                newName: "IX_NumeroVillas_VillaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeroVillas",
                table: "NumeroVillas",
                column: "VillaNo");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 13, 15, 30, 28, 790, DateTimeKind.Local).AddTicks(7679), new DateTime(2023, 9, 13, 15, 30, 28, 790, DateTimeKind.Local).AddTicks(7637) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 13, 15, 30, 28, 790, DateTimeKind.Local).AddTicks(7684), new DateTime(2023, 9, 13, 15, 30, 28, 790, DateTimeKind.Local).AddTicks(7683) });

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroVillas_Villas_VillaId",
                table: "NumeroVillas",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroVillas_Villas_VillaId",
                table: "NumeroVillas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeroVillas",
                table: "NumeroVillas");

            migrationBuilder.RenameTable(
                name: "NumeroVillas",
                newName: "numerovillas");

            migrationBuilder.RenameIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "numerovillas",
                newName: "IX_numerovillas_VillaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_numerovillas",
                table: "numerovillas",
                column: "VillaNo");

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

            migrationBuilder.AddForeignKey(
                name: "FK_numerovillas_Villas_VillaId",
                table: "numerovillas",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
