using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBotGUI
{
    class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; } // For True/False, use 0 or 1
        public string Explanation { get; set; }
    }
}
