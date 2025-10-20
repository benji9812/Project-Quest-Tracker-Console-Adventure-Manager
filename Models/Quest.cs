using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Models
{
    public enum Priority { High, Medium, Low }

    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}