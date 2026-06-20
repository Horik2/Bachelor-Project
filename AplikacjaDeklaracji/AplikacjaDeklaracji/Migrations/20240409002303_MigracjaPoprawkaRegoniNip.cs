using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacjaDeklaracji.Migrations
{
    /// <inheritdoc />
    public partial class MigracjaPoprawkaRegoniNip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Identyfikator REGON",
                table: "DaneOsobowe",
                type: "int",
                unicode: false,
                fixedLength: true,
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(14)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Identyfikator NIP",
                table: "DaneOsobowe",
                type: "int",
                unicode: false,
                fixedLength: true,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identyfikator REGON",
                table: "DaneOsobowe",
                type: "char(14)",
                unicode: false,
                fixedLength: true,
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Identyfikator NIP",
                table: "DaneOsobowe",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
