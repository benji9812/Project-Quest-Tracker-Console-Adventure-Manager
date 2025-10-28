using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Models
{
    public class Hero // Representerar en hjälte/användare
    {
        public string Username { get; set; } // Hjältens användarnamn
        public string PasswordHash { get; set; } // Hashat lösenord
        public string Email { get; set; } // Hjältens e-postadress
        public string Phone { get; set; } // Hjältens telefonnummer
    }
}
