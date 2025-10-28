using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Quest_Tracker___Console_Adventure_Manager.Models;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Services
{
    public class QuestManager // Hanterar uppdrag
    {
        private List<Quest> quests = new List<Quest>(); // Lista över uppdrag

        public void AddQuest(Quest quest) // Lägg till nytt uppdrag
        {
            quests.Add(quest); // Lägg till uppdrag i listan
        }

        public List<Quest> GetAllQuests() // Hämta alla uppdrag
        {
            return quests; // Returnera listan med uppdrag
        }

        public bool CompleteQuest(string title) // Markera uppdrag som slutfört
        {
            var quest = quests.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Hitta uppdrag efter titel
            if (quest != null && !quest.IsCompleted) // Om uppdraget finns och ej är slutfört
            {
                quest.IsCompleted = true; // Markera som slutfört
                return true; // Returnera true för framgång
            }
            return false; // Returnera false om uppdraget inte hittades eller redan var slutfört
        }

        public bool UpdateQuest(string title, string newDesc, DateTime newDueDate, Priority newPriority) // Uppdatera uppdragsdetaljer
        {
            var quest = quests.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Hitta uppdrag efter titel
            if (quest != null) // Om uppdraget finns
            {
                quest.Description = newDesc;
                quest.DueDate = newDueDate;
                quest.Priority = newPriority;
                return true; // Returnera true för framgång
            }
            return false; // Returnera false om uppdraget inte hittades
        }
    }
}
