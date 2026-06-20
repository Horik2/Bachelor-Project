using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaDeklaracji.Models
{
    [Table("Deklaracje")]
    public class Deklaracja
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public bool IsActual { get; set; }

        public int DaneOsoboweId { get; set; }
        public DaneOsobowe DaneOsobowe { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
