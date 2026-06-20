using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacjaDeklaracji.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaDeklaracji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pesel = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Nazwisko = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Imie = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Drugieimie = table.Column<string>(name: "Drugie imie", type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Imieojca = table.Column<string>(name: "Imie ojca", type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Imiematki = table.Column<string>(name: "Imie matki", type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Dataurodzenia = table.Column<DateTime>(name: "Data urodzenia", type: "date", nullable: false),
                    PelnaNazwa = table.Column<string>(name: "Pelna Nazwa", type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    IdentyfikatorREGON = table.Column<string>(name: "Identyfikator REGON", type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: true),
                    IdentyfikatorNIP = table.Column<string>(name: "Identyfikator NIP", type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    Kraj = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Wojewodztwo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Powiat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Gmina = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ulica = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nrdomu = table.Column<int>(name: "Nr domu", type: "int", nullable: false),
                    Nrlokalu = table.Column<int>(name: "Nr lokalu", type: "int", nullable: true),
                    Miejscowosc = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Kodpocztowy = table.Column<string>(name: "Kod pocztowy", type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Nrtelefonu = table.Column<string>(name: "Nr telefonu", type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    Adresemail = table.Column<string>(name: "Adres e-mail", type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Stawkazaos = table.Column<decimal>(name: "Stawka za os", type: "decimal(10,2)", nullable: false),
                    Liczbaosob = table.Column<int>(name: "Liczba osob", type: "int", nullable: false),
                    Kwotazwolnienia = table.Column<decimal>(name: "Kwota zwolnienia", type: "decimal(10,2)", nullable: true, defaultValueSql: "((0))"),
                    Wysokoscoplaty = table.Column<decimal>(name: "Wysokosc oplaty", type: "decimal(21,2)", nullable: true, computedColumnSql: "([Stawka za os]*nullif([Liczba osob],(0)))", stored: false),
                    Wysokoscpozwol = table.Column<decimal>(name: "Wysokosc po zwol", type: "decimal(22,2)", nullable: true, computedColumnSql: "([Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia])", stored: false),
                    Kosztostateczny = table.Column<decimal>(name: "Koszt ostateczny", type: "decimal(22,2)", nullable: true, computedColumnSql: "(case when [Kwota zwolnienia]=NULL then [Stawka za os]*nullif([Liczba osob],(0)) else [Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia] end)", stored: false),
                    Kwartalnaoplata = table.Column<decimal>(name: "Kwartalna oplata", type: "decimal(24,2)", nullable: true, computedColumnSql: "(case when [Kwota zwolnienia]=NULL then [Stawka za os]*nullif([Liczba osob],(0)) else [Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia] end*(4))", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TabelaDe__3214EC0731531B25", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaDeklaracji");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
