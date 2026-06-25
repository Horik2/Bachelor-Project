using AplikacjaDeklaracji.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AplikacjaDeklaracji.Services
{
    public class LogService 
    {
        
        private readonly ApplicationDbContext _context;
        public LogService( ApplicationDbContext context) 
        {
            _context = context;
        }

        static public void AddLog(int userID, TypZdarzenia TypZdarzenia, string Wiadomosc, string Opis)
        {

            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "PLACEHOLDER_CONSTRING";
            Conn.Open();

            SqlCommand Comm = new SqlCommand("INSERT INTO [LOGI] ([USER_ID], [TYP_ZDARZENIA], [WIADOMOSC],[OPIS]) VALUES (@USER_ID, @TYP_ZDARZENIA, @WIADOMOSC, @OPIS)", Conn);
            Comm.Parameters.AddWithValue("USER_ID", userID);
            Comm.Parameters.AddWithValue("TYP_ZDARZENIA", TypZdarzenia);
            Comm.Parameters.AddWithValue("Wiadomosc", Wiadomosc);

            Comm.Parameters.AddWithValue("Opis", Opis);
            Comm.ExecuteReader();

            Conn.Close();
        }

        
    }
}
