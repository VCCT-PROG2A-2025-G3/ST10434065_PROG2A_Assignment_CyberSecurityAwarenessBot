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
    // This class represents a quiz question with its options, correct answer index, and explanation.
    class QuizQuestion
    {
        #region Properties
        // Properties for the quiz question
        public string Question { get; set; } // The question text
        public List<string> Options { get; set; } // List of answer options for the question
        public int CorrectIndex { get; set; } // For Correct Amswer, use 0 or 1
        public string Explanation { get; set; } // Explanation of the correct answer
        #endregion
    }
}
