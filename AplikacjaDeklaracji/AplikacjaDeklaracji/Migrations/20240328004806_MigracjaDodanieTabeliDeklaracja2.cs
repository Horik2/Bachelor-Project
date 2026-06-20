using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacjaDeklaracji.Migrations
{
    /// <inheritdoc />
    public partial class MigracjaDodanieTabeliDeklaracja2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deklaracjas_DaneOsobowe_DaneOsoboweId",
                table: "Deklaracjas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deklaracjas_User_UserId",
                table: "Deklaracjas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deklaracjas",
                table: "Deklaracjas");

            migrationBuilder.RenameTable(
                name: "Deklaracjas",
                newName: "Deklaracje");

            migrationBuilder.RenameIndex(
                name: "IX_Deklaracjas_UserId",
                table: "Deklaracje",
                newName: "IX_Deklaracje_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deklaracjas_DaneOsoboweId",
                table: "Deklaracje",
                newName: "IX_Deklaracje_DaneOsoboweId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deklaracje",
                table: "Deklaracje",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deklaracje_DaneOsobowe_DaneOsoboweId",
                table: "Deklaracje",
                column: "DaneOsoboweId",
                principalTable: "DaneOsobowe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deklaracje_User_UserId",
                table: "Deklaracje",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deklaracje_DaneOsobowe_DaneOsoboweId",
                table: "Deklaracje");

            migrationBuilder.DropForeignKey(
                name: "FK_Deklaracje_User_UserId",
                table: "Deklaracje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deklaracje",
                table: "Deklaracje");

            migrationBuilder.RenameTable(
                name: "Deklaracje",
                newName: "Deklaracjas");

            migrationBuilder.RenameIndex(
                name: "IX_Deklaracje_UserId",
                table: "Deklaracjas",
                newName: "IX_Deklaracjas_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deklaracje_DaneOsoboweId",
                table: "Deklaracjas",
                newName: "IX_Deklaracjas_DaneOsoboweId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deklaracjas",
                table: "Deklaracjas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deklaracjas_DaneOsobowe_DaneOsoboweId",
                table: "Deklaracjas",
                column: "DaneOsoboweId",
                principalTable: "DaneOsobowe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deklaracjas_User_UserId",
                table: "Deklaracjas",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
