using Project_Quest_Tracker___Console_Adventure_Manager.Models;
using Project_Quest_Tracker___Console_Adventure_Manager.Services;
using System;

namespace Project_Quest_Tracker___Console_Adventure_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej och välkommen till Guild Terminal!"); // Huvudmeny för hjältehantering och quest-tracking
            var auth = new AuthManager(); // Autentisering
            var questManager = new QuestManager(); // Quest-hantering
            Hero loggedInHero = null; // Inloggad hjälte
            while (true) // Huvudloop
            {
                if (loggedInHero == null) // Om ingen hjälte är inloggad
                {
                    Console.WriteLine("Vänligen välj ett alternativ: ");
                    Console.WriteLine("1. Registrera hjälte\n2. Logga in hjälte\n3. Avsluta");
                    var choice = Console.ReadLine();
                    if (choice == "1") // Registrering
                    {
                        Console.Write("Hjältenamn: ");
                        var username = Console.ReadLine(); // Användarnamn
                        Console.Write("Lösenord (Minst 6 tecken, en stor bokstav, en siffra, ett specialtecken): ");
                        var password = Console.ReadLine(); // Lösenord
                        Console.Write("Email: ");
                        var email = Console.ReadLine(); // Email
                        Console.Write("Telefon: ");
                        var phone = Console.ReadLine(); // Telefon

                        var hero = auth.RegisterHero(username, password, email, phone); // Registrera hjälte
                        if (hero != null) // Kontrollera om registrering lyckades
                            Console.WriteLine($"Välkommen till Guild Terminal, {username}!");
                        else
                            Console.WriteLine("Registrering misslyckades! Kontrollera användarnamn och lösenord.");
                    }
                    else if (choice == "2") // Inloggning
                    {
                        Console.Write("Hjältenamn: ");
                        var username = Console.ReadLine();
                        Console.Write("Lösenord: ");
                        var password = Console.ReadLine();
                        var hero = auth.LoginHero(username, password); // Försök logga in hjälte

                        if (hero != null) // Inloggning lyckades
                        {
                            // 2FA - generera kod och skicka till email
                            string sentCode = auth.Send2FACode(hero.Email); // Skapar 2FA-kod och skickar mail
                            Console.WriteLine("En kod har skickats till din email. Ange koden:");
                            string userCode = Console.ReadLine(); // Användarens inmatade kod

                            if (userCode == sentCode) // Kontrollera koden
                            {
                                Console.WriteLine($"Välkommen tillbaka till Guild Terminal, {username}!");
                                loggedInHero = hero; // Sätt inloggad hjälte

                                // Visa quests direkt efter inloggning om du vill
                                var quests = questManager.GetAllQuests(); // Hämta alla quests
                                foreach (var q in quests) // Visa quests
                                    Console.WriteLine($"{q.Title} – {q.Description} – Deadline: {q.DueDate:yyyy-MM-dd} – Prioritet: {q.Priority} – Klar: {q.IsCompleted}");

                                // Guild alerts
                                var notificationManager = new NotificationManager(); // Notifikationshanterare
                                notificationManager.ShowNearDeadlineQuests(quests); // Visa quests med nära deadline
                            }
                            else
                            {
                                Console.WriteLine("Fel kod! Inloggning misslyckades.");
                            }
                        }
                            else
                            {
                                Console.WriteLine("Fel användarnamn eller lösenord.");
                            }
                    }

                    else if (choice == "3") // Avsluta program
                        break;
                }
                else // Hjälte är inloggad
                {
                    Console.WriteLine( // Meny för quest-hantering
                        "1. Lägg till nytt quest\n" +
                        "2. Visa alla quests\n" +
                        "3. Uppdatera/Avsluta quest\n" +
                        "4. Logga ut\n" +
                        "5. Visa Guild-rapport");
                    var val = Console.ReadLine();

                    if (val == "1") // Lägg till nytt quest
                    {
                        Console.Write("Titel: ");
                        var titel = Console.ReadLine(); // Quest-titel

                        // Fråga om AI-beskrivning
                        Console.WriteLine("Vill du att Guild Advisor (AI) ska generera en beskrivning? (j/n):");
                        var aiVal = Console.ReadLine(); // Användarens val för AI-beskrivning

                        string beskrivning; // Quest-beskrivning
                        if (aiVal.ToLower() == "j") // Använd AI för beskrivning
                        {
                            var ai = new AIAssistant(); // Skapa AI-assistent
                            beskrivning = ai.GenerateDescription(titel); // Generera beskrivning
                            Console.WriteLine($"AI-beskrivning satt: {beskrivning}");
                        }
                        else
                        {
                            Console.Write("Beskrivning: ");
                            beskrivning = Console.ReadLine();
                        }

                        Console.Write("Deadline (ÅÅÅÅ-MM-DD): ");
                        var dat = DateTime.Parse(Console.ReadLine()); // Deadline
                        Console.Write("Prioritet (High/Medium/Low): ");
                        var pri = Enum.Parse<Priority>(Console.ReadLine(), true); // Prioritet

                        var quest = new Quest // Skapa nytt quest-objekt
                        {
                            Title = titel,
                            Description = beskrivning,
                            DueDate = dat,
                            Priority = pri,
                            IsCompleted = false
                        };

                        questManager.AddQuest(quest); // Lägg till quest
                        Console.WriteLine("Quest skapad!");
                    }
                    else if (val == "2") // Visa alla quests
                    {
                        var quests = questManager.GetAllQuests(); // Hämta alla quests
                        foreach (var q in quests) // Visa quests
                            Console.WriteLine($"{q.Title} – {q.Description} – Deadline: {q.DueDate:yyyy-MM-dd} – Prioritet: {q.Priority} – Klar: {q.IsCompleted}");
                    }
                    else if (val == "3") // Uppdatera eller avsluta quest
                    {
                        Console.Write("Quest-titel: ");
                        var titel = Console.ReadLine(); // Quest-titel att uppdatera/avsluta
                        Console.WriteLine("1. Markera som klar\n2. Uppdatera quest");
                        var akt = Console.ReadLine(); // Användarens val för åtgärd
                        if (akt == "1") // Markera som klar
                        {
                            if (questManager.CompleteQuest(titel)) // Försök markera som klar
                                Console.WriteLine("Quest markerat som klar!");
                            else
                                Console.WriteLine("Hittade inte quest eller redan klart.");
                        }
                        else if (akt == "2") // Uppdatera quest
                        {
                            Console.Write("Ny beskrivning: ");
                            var beskrivning = Console.ReadLine(); // Ny beskrivning
                            Console.Write("Ny deadline (ÅÅÅÅ-MM-DD): ");
                            var dat = DateTime.Parse(Console.ReadLine()); // Ny deadline
                            Console.Write("Ny prioritet (High/Medium/Low): ");
                            var pri = Enum.Parse<Priority>(Console.ReadLine(), true); // Ny prioritet
                            if (questManager.UpdateQuest(titel, beskrivning, dat, pri)) // Försök uppdatera quest
                                Console.WriteLine("Quest uppdaterat!");
                            else
                                Console.WriteLine("Hittade inte quest.");
                        }
                    }
                    else if (val == "4") // Logga ut
                    {
                        loggedInHero = null; // Sätt inloggad hjälte till null
                        Console.WriteLine("Utloggad!");
                    }
                    // KODEN FÖR RAPPORT:
                    var allQuests = questManager.GetAllQuests(); // Hämta alla quests
                    int active = allQuests.Count(q => !q.IsCompleted); // Räkna pågående quests
                    int completed = allQuests.Count(q => q.IsCompleted); // Räkna fullförda quests
                    int urgent = allQuests.Count(q => !q.IsCompleted && (q.DueDate - DateTime.Now).TotalHours < 24); // Räkna brådskande quests

                    Console.WriteLine($"Du har {active} pågående quests, {urgent} måste slutföras idag, {active - urgent} är under kontroll. {completed} fullförda quests.");
                    }
            }
        }
    }
}

