using Project_Quest_Tracker___Console_Adventure_Manager.Models;
using Project_Quest_Tracker___Console_Adventure_Manager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Services
{
    public class AuthManager // Hanterar registrering, inloggning och 2FA
    {
        private List<Hero> heroes = new List<Hero>(); // Simulerad databas för hjältar

        public bool IsPasswordStrong(string pwd) // Kontrollera lösenordets styrka
        {
            return pwd.Length >= 6 && // Minst 6 tecken
                   pwd.Any(char.IsUpper) && // Minst en stor bokstav
                   pwd.Any(char.IsDigit) && // Minst en siffra
                   pwd.Any(ch => !char.IsLetterOrDigit(ch)); // Minst ett specialtecken
        }

        public string HashPassword(string pwd) // Enkel hashfunktion (vänd strängen)
        {
            // Här kan du använda bättre hash i framtiden (SHA256 etc)
            return string.Concat(pwd.Reverse()); // Vänder bara strängen som en placeholder
        }

        public Hero RegisterHero(string username, string pwd, string email, string phone) // Registrera ny hjälte
        {
            if (!IsPasswordStrong(pwd)) // Kontrollera lösenordets styrka
                return null; // Om lösenordet är svagt, returnera null
            if (heroes.Any(h => h.Username == username)) // Kontrollera om användarnamnet redan finns
                return null; // Om användarnamnet finns, returnera null

            var hero = new Hero // Skapa ny hjälte
            {
                Username = username,
                PasswordHash = HashPassword(pwd),
                Email = email,
                Phone = phone
            };
            heroes.Add(hero); // Lägg till hjälten i "databasen"
            return hero; // Returnera den registrerade hjälten
        }

        public Hero LoginHero(string username, string pwd) // Logga in hjälte
        {
            var hero = heroes.FirstOrDefault(h => h.Username == username); // Hitta hjälte efter användarnamn
            if (hero != null && hero.PasswordHash == HashPassword(pwd)) // Kontrollera lösenord
                return hero; // Returnera hjälten om inloggningen lyckades
            return null; // Returnera null om inloggningen misslyckades
        }

        public string Send2FACode(string email) // Skicka 2FA-kod via e-post
        {
            var code = new Random().Next(100000, 999999).ToString(); // Generera en 6-siffrig kod
            EmailSender.SendEmail(email, "Guild Terminal 2FA-kod", $"Din kod: {code}"); // Skicka e-post med koden
            return code; // Returnera koden för verifiering
        }
    }
}