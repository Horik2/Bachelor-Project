using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacjaDeklaracji.Migrations
{
    /// <inheritdoc />
    public partial class MigracjaDodanieTabeliDeklaracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeklaracjaId",
                table: "DaneOsobowe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Deklaracjas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActual = table.Column<bool>(type: "bit", nullable: false),
                    DaneOsoboweId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deklaracjas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deklaracjas_DaneOsobowe_DaneOsoboweId",
                        column: x => x.DaneOsoboweId,
                        principalTable: "DaneOsobowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deklaracjas_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deklaracjas_DaneOsoboweId",
                table: "Deklaracjas",
                column: "DaneOsoboweId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deklaracjas_UserId",
                table: "Deklaracjas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deklaracjas");

            migrationBuilder.DropColumn(
                name: "DeklaracjaId",
                table: "DaneOsobowe");
        }
    }
}
