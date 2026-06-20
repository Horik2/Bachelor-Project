using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AplikacjaDeklaracji.Models;

namespace AplikacjaDeklaracji.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DaneOsobowe> daneOsobowes { get; set; }
        public DbSet<Lokale> Lokales { get; set; }
        public DbSet<Deklaracja> Deklaracjas { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("name=constring");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                     .HasIndex(u => u.Username)
                     .IsUnique();

            modelBuilder.Entity<DaneOsobowe>()
                    .HasMany(d => d.Lokale)
                    .WithOne(l => l.DaneOsobowe)
                    .HasForeignKey(l => l.DaneOsoboweId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DaneOsobowe>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("DaneOsobowe");

                entity.Property(e => e.AdresEMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Adres e-mail");
                entity.Property(e => e.DataUrodzenia)
                    .HasColumnType("date")
                    .HasColumnName("Data urodzenia");
                entity.Property(e => e.DrugieImie)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Drugie imie");
                entity.Property(e => e.IdentyfikatorNip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("Identyfikator NIP");
                entity.Property(e => e.IdentyfikatorRegon)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("Identyfikator REGON");
                entity.Property(e => e.Imie)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.ImieMatki)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Imie matki");
                entity.Property(e => e.ImieOjca)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Imie ojca");
                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.NrTelefonu)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("Nr telefonu");
                entity.Property(e => e.PelnaNazwa)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Pelna Nazwa");
                entity.Property(e => e.Pesel)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();
            });


            //Lokale ===================================================================


            modelBuilder.Entity<Lokale>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Lokale");

                
                entity.Property(e => e.KodPocztowy)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("Kod pocztowy");
                
                entity.Property(e => e.KosztOstateczny)
                    .HasComputedColumnSql("(case when [Kwota zwolnienia]=NULL then [Stawka za os]*nullif([Liczba osob],(0)) else [Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia] end)", false)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("Koszt ostateczny");
                entity.Property(e => e.Kraj)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.KwartalnaOplata)
                    .HasComputedColumnSql("(case when [Kwota zwolnienia]=NULL then [Stawka za os]*nullif([Liczba osob],(0)) else [Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia] end*(4))", false)
                    .HasColumnType("decimal(24, 2)")
                    .HasColumnName("Kwartalna oplata");
                entity.Property(e => e.KwotaZwolnienia)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Kwota zwolnienia");
                entity.Property(e => e.LiczbaOsob).HasColumnName("Liczba osob");
                entity.Property(e => e.Miejscowosc)
                    .HasMaxLength(80)
                    .IsUnicode(false);
                entity.Property(e => e.NrDomu).HasColumnName("Nr domu");
                entity.Property(e => e.NrLokalu).HasColumnName("Nr lokalu");
                entity.Property(e => e.Powiat)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StawkaZaOs)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Stawka za os");
                entity.Property(e => e.Ulica)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Wojewodztwo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.WysokoscOplaty)
                    .HasComputedColumnSql("([Stawka za os]*nullif([Liczba osob],(0)))", false)
                    .HasColumnType("decimal(21, 2)")
                    .HasColumnName("Wysokosc oplaty");
                entity.Property(e => e.WysokoscPoZwol)
                    .HasComputedColumnSql("([Stawka za os]*nullif([Liczba osob],(0))-[Kwota zwolnienia])", false)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("Wysokosc po zwol");

                entity.Property(e => e.Gmina)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deklaracja>()
                .HasOne(e => e.User)
                .WithMany(e => e.Deklaracje)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DaneOsobowe>()
                .HasOne(e => e.Deklaracja)
                .WithOne(e => e.DaneOsobowe)
                .HasForeignKey<Deklaracja>(e => e.DaneOsoboweId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
