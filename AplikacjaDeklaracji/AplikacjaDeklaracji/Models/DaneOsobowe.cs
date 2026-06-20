using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplikacjaDeklaracji.Models
{
    public class DaneOsobowe
    {
        public int Id { get; set; }

        [MaxLength(11, ErrorMessage = "Pesel może składać się maksymalnie z 11 cyfr.")]
        public string Pesel { get; set; } = null!;

        public string Nazwisko { get; set; } = null!;

        public string Imie { get; set; } = null!;

        public string? DrugieImie { get; set; }

        public string ImieOjca { get; set; } = null!;

        public string ImieMatki { get; set; } = null!;

        public DateTime DataUrodzenia { get; set; }

        public string? PelnaNazwa { get; set; }

        [MaxLength(9, ErrorMessage = "Identyfikator Regon może składać się maksymalnie z 9 cyfr.")]
        public string? IdentyfikatorRegon { get; set; }

        [MaxLength(12, ErrorMessage = "Identyfikator nip może składać się maksymalnie z 12 cyfr.")]
        public string? IdentyfikatorNip { get; set; }

        [MaxLength(9, ErrorMessage = "Numer telefonu może składać się maksymalnie z 9 cyfr.")]
        public string? NrTelefonu { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
        public string? AdresEMail { get; set; }

        //nawigacyjne
        public ICollection<Lokale> Lokale { get; set; } = new List<Lokale>();

        public Deklaracja Deklaracja { get; set; }
        public int DeklaracjaId { get; set; }
    }
}
