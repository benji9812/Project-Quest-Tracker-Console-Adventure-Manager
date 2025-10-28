using Project_Quest_Tracker___Console_Adventure_Manager.Models;
using System;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Services
{
    public class NotificationManager // Hanterar notifikationer för uppdrag
    {
        public void NotifyQuestDeadline(Quest q) // Notifiera om uppdrag nära deadline
        {
            TimeSpan diff = q.DueDate - DateTime.Now; // Beräkna tid kvar till deadline
            if (!q.IsCompleted && diff.TotalHours < 24 && diff.TotalHours > 0) // Om mindre än 24h kvar och ej slutfört
            {
                Console.WriteLine($"⚔️ Guild Alert: Ditt uppdrag '{q.Title}' måste vara klart inom 24h ({q.DueDate:yyyy-MM-dd HH:mm})!");
            }
        }

        public void ShowNearDeadlineQuests(List<Quest> quests) // Visa alla uppdrag nära deadline
        {
            foreach (var q in quests) // Iterera genom uppdragen
            {
                NotifyQuestDeadline(q); // Anropa notifieringsmetoden
            }
        }
    }
}