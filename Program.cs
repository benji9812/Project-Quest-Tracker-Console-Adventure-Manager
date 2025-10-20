using Project_Quest_Tracker___Console_Adventure_Manager.Services;
using Project_Quest_Tracker___Console_Adventure_Manager.Models;
using System;

namespace Project_Quest_Tracker___Console_Adventure_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej och välkommen till Guild Terminal!");
            var auth = new AuthManager();
            var questManager = new QuestManager();
            Hero loggedInHero = null;
            while (true)
            {
                if (loggedInHero == null)
                {
                Console.WriteLine("Vänligen välj ett alternativ: ");
                Console.WriteLine("1. Registrera hjälte\n2. Logga in hjälte\n3. Avsluta");
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.Write("Hjältenamn: ");
                    var username = Console.ReadLine();
                    Console.Write("Lösenord (Minst 6 tecken, en stor bokstav, en siffra, ett specialtecken): ");
                    var password = Console.ReadLine();
                    Console.Write("Email: ");
                    var email = Console.ReadLine();
                    Console.Write("Telefon: ");
                    var phone = Console.ReadLine();

                    var hero = auth.RegisterHero(username, password, email, phone);
                    if (hero != null)
                        Console.WriteLine($"Välkommen till Guild Terminal, {username}!");
                    else
                        Console.WriteLine("Registrering misslyckades! Kontrollera användarnamn och lösenord.");
                }
                else if (choice == "2")
                {
                    Console.Write("Hjältenamn: ");
                    var username = Console.ReadLine();
                    Console.Write("Lösenord: ");
                    var password = Console.ReadLine();
                    var hero = auth.LoginHero(username, password);
                    if (hero != null)
                    {
                        Console.WriteLine($"Välkommen tillbaka till Guild Terminal, {username}!");
                        loggedInHero = hero;
                    }
                    else
                        Console.WriteLine("Fel användarnamn eller lösenord.");
                }
                else if (choice == "3")
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "1. Lägg till nytt quest\n" +
                        "2. Visa alla quests\n" +
                        "3. Uppdatera/Avsluta quest\n" +
                        "4. Logga ut");
                    var val = Console.ReadLine();

                if (val == "1")
                {
                    Console.Write("Titel: ");
                    var titel = Console.ReadLine();
                    Console.Write("Beskrivning: ");
                    var beskrivning = Console.ReadLine();
                    Console.Write("Deadline (ÅÅÅÅ-MM-DD): ");
                    var dat = DateTime.Parse(Console.ReadLine());
                    Console.Write("Prioritet (High/Medium/Low): ");
                    var pri = Enum.Parse<Priority>(Console.ReadLine(), true);

                    var quest = new Quest { Title = titel, Description = beskrivning, DueDate = dat, Priority = pri, IsCompleted = false };
                    questManager.AddQuest(quest);
                    Console.WriteLine("Quest skapad!");
                }
                else if (val == "2")
                {
                    var quests = questManager.GetAllQuests();
                    foreach (var q in quests)
                        Console.WriteLine($"{q.Title} – {q.Description} – Deadline: {q.DueDate:yyyy-MM-dd} – Prioritet: {q.Priority} – Klar: {q.IsCompleted}");
                }
                else if (val == "3")
                {
                    Console.Write("Quest-titel: ");
                    var titel = Console.ReadLine();
                    Console.WriteLine("1. Markera som klar\n2. Uppdatera quest");
                    var akt = Console.ReadLine();
                    if (akt == "1")
                    {
                        if (questManager.CompleteQuest(titel))
                            Console.WriteLine("Quest markerat som klar!");
                        else
                            Console.WriteLine("Hittade inte quest eller redan klart.");
                    }
                    else if (akt == "2")
                    {
                        Console.Write("Ny beskrivning: ");
                        var beskrivning = Console.ReadLine();
                        Console.Write("Ny deadline (ÅÅÅÅ-MM-DD): ");
                        var dat = DateTime.Parse(Console.ReadLine());
                        Console.Write("Ny prioritet (High/Medium/Low): ");
                        var pri = Enum.Parse<Priority>(Console.ReadLine(), true);
                        if (questManager.UpdateQuest(titel, beskrivning, dat, pri))
                            Console.WriteLine("Quest uppdaterat!");
                        else
                            Console.WriteLine("Hittade inte quest.");
                    }
                }
                else if (val == "4")
                {
                    loggedInHero = null;
                    Console.WriteLine("Utloggad!");
                    }
                }
            }

        }
    }
}
