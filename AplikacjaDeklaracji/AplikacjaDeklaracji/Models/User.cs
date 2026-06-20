using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AplikacjaDeklaracji.Models
{
    
        public class User
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "Pole nazwa użytkownika jest wymagane.")]
            public string Username { get; set; }
            [Required(ErrorMessage = "Pole hasło jest wymagane.")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Hasło musi mieć co najmniej 8 znaków, zawierać przynajmniej jedną literę i jedną cyfrę.")]
            public string PasswordHash { get; set; }
            public string Salt { get; set; }

            public ICollection<Deklaracja> Deklaracje { get; set; } = new List<Deklaracja>();
    }
}
