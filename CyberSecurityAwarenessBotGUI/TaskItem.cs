/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT POE 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBotGUI
{
    public class TaskItem
    {
        #region Fields
        public string Title { get; set; } // Title of the task item
        public string Description { get; set; } // Description of the task item
        public DateTime? Reminder { get; set; } // Optional reminder date and time for the task item
        public bool IsCompleted { get; set; } = false; // Indicates whether the task item is completed or not, default is false
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region ToString ToggleCompletion
        // Constructor to initialize a task item with a title, description and reminder date and time.
        public override string ToString()
        {
            string status = IsCompleted ? "[✔]" : "[ ]"; // Determine the status of the task item, completed or not
            string display = $"{status} {Title}\n{Description}"; // Format the display string with title and description
            if (Reminder.HasValue) // If a reminder is set, append it to the display string
            {
                display += $"\nReminder: {Reminder.Value:g}"; // Use 'g' format for general date and time representation
            }
            return display; // Return the formatted string representation of the task item.
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region TaskItem Constructor
        public TaskItem(string title, string description = "", DateTime? reminder = null)
        {
            Title = title;
            Description = description;
            Reminder = reminder;
            IsCompleted = false;
        }
        #endregion
    }
}
