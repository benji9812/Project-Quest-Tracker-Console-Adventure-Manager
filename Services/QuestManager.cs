using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Quest_Tracker___Console_Adventure_Manager.Models;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Services
{
    public class QuestManager
    {
        private List<Quest> quests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public List<Quest> GetAllQuests()
        {
            return quests;
        }

        public bool CompleteQuest(string title)
        {
            var quest = quests.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (quest != null && !quest.IsCompleted)
            {
                quest.IsCompleted = true;
                return true;
            }
            return false;
        }

        public bool UpdateQuest(string title, string newDesc, DateTime newDueDate, Priority newPriority)
        {
            var quest = quests.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (quest != null)
            {
                quest.Description = newDesc;
                quest.DueDate = newDueDate;
                quest.Priority = newPriority;
                return true;
            }
            return false;
        }
    }
}
