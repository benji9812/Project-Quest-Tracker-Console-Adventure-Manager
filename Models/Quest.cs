using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Quest_Tracker___Console_Adventure_Manager.Models
{
    public enum Priority { High, Medium, Low } // Prioritetsnivåer för uppdrag

    public class Quest // Modell för ett uppdrag
    {
        public string Title { get; set; } // Uppdragets titel
        public string Description { get; set; } // Uppdragets beskrivning
        public DateTime DueDate { get; set; } // Uppdragets deadline
        public Priority Priority { get; set; } // Uppdragets prioritet
        public bool IsCompleted { get; set; } // Om uppdraget är slutfört
    }
}