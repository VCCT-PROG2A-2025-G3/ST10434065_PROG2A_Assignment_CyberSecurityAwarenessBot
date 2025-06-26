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
    // This class represents a task item with a description and completion status.
    class TaskManager
    {
        #region Tasks List
        // A simple task item method to represent each task
        private List<TaskItem> tasks = new List<TaskItem>();
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Add Task Constructor
        // Constructor to initialize the task manager
        public void AddTask(string description)
        {
            tasks.Add(new TaskItem(description)); // Add a new task with the given description to the list
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Tasks
        // This method returns the list of tasks
        public List<TaskItem> GetTasks()
        {
            return tasks; // Return the list of tasks
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Mark Task Complete
        // This method marks a task as complete based on its index
        public void MarkTaskComplete(int index)
        {
            if (index >= 0 && index < tasks.Count) // Check if the index is valid
            {
                tasks[index].IsCompleted = true; // Set the task's completion status to true
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Remove Task
        // This method removes a task from the list based on its index
        public void RemoveTask(int index)
        {
            if (index >= 0 && index < tasks.Count) // Check if the index is valid
            { 
                tasks.RemoveAt(index); // Remove the task at the specified index
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Clear All Tasks
        // This method clears all tasks from the list
        public void ClearAll()
        {
            tasks.Clear(); // Clear the list of tasks
        }
        #endregion
    }
}
