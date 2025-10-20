using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Quest_Tracker___Console_Adventure_Manager.Models;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Services
{
    public class AuthManager
    {
        private List<Hero> heroes = new List<Hero>();

        public bool IsPasswordStrong(string pwd) // Minst 6 tecken, en stor bokstav, en siffra, ett specialtecken
        {
            return pwd.Length >= 6 &&
                   pwd.Any(char.IsUpper) &&
                   pwd.Any(char.IsDigit) &&
                   pwd.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public string HashPassword(string pwd)
        {
            // Här kan du använda bättre hash i framtiden (SHA256 etc)
            return string.Concat(pwd.Reverse());
        }

        public Hero RegisterHero(string username, string pwd, string email, string phone)
        {
            if (!IsPasswordStrong(pwd))
                return null;
            if (heroes.Any(h => h.Username == username))
                return null;
            var hero = new Hero
            {
                Username = username,
                PasswordHash = HashPassword(pwd),
                Email = email,
                Phone = phone
            };
            heroes.Add(hero);
            return hero;
        }

        public Hero LoginHero(string username, string pwd)
        {
            var hero = heroes.FirstOrDefault(h => h.Username == username);
            if (hero != null && hero.PasswordHash == HashPassword(pwd))
                return hero;
            return null;
        }
    }
}