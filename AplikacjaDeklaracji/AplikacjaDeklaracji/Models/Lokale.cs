using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AplikacjaDeklaracji.Models
{
    public class Lokale
    {
        public int Id { get; set; }

        public string Kraj { get; set; } = null!;

        public string Wojewodztwo { get; set; } = null!;

        public string Powiat { get; set; } = null!;

        public string Gmina { get; set; } = null!;

        public string Ulica { get; set; } = null!;

        public int NrDomu { get; set; }

        public int? NrLokalu { get; set; }

        public string Miejscowosc { get; set; } = null!;

        public string KodPocztowy { get; set; } = null!;

        public decimal StawkaZaOs { get; set; }

        public int LiczbaOsob { get; set; }

        public decimal? KwotaZwolnienia { get; set; }

        public decimal? WysokoscOplaty { get; set; }

        public decimal? WysokoscPoZwol { get; set; }

        public decimal? KosztOstateczny { get; set; }

        public decimal? KwartalnaOplata { get; set; }

        //nawigacyjne
        public int DaneOsoboweId { get; set; } 
        public DaneOsobowe DaneOsobowe { get; set; }
    }
}
