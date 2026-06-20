using Microsoft.Data.SqlClient;
namespace AplikacjaDeklaracji.Models
{
    public enum TypZdarzenia : int
    {
        Error = 1,
        Warning = 2,
        Information = 0
    }
}
