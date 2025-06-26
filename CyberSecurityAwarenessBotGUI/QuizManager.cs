using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBotGUI
{
    class QuizManager
    {
        public static List<QuizQuestion> Questions = new List<QuizQuestion>
        {
            new QuizQuestion
            {
            Question = "What should you do if you receive an email asking for your password?",
            Options = new List<string> { "Reply", "Ignore", "Report", "Delete" },
            CorrectIndex = 2,
            Explanation = "You should report suspicious emails as phishing."
            },
            new QuizQuestion
            {
            Question = "Using '123456' as your password is safe. (True/False)",
            Options = new List<string> { "True", "False" },
            CorrectIndex = 1,
            Explanation = "'123456' is one of the most common and weakest passwords."
            },
            new QuizQuestion
            {
            Question = "Which of the following is a strong password?",
            Options = new List<string> { "password", "12345678", "LetMeIn", "G#3b$9!kP2" },
            CorrectIndex = 3,
            Explanation = "A strong password includes upper/lowercase letters, numbers, and symbols."
            },
            new QuizQuestion
            {
            Question = "Public Wi-Fi is always safe for online banking. (True/False)",
            Options = new List<string> { "True", "False" },
            CorrectIndex = 1,
            Explanation = "Public Wi-Fi can be insecure and prone to man-in-the-middle attacks."
            },
            new QuizQuestion
            {
            Question = "What is phishing?",
            Options = new List<string> {
                "A type of fish",
                "Tricking users to reveal personal info",
                "Antivirus software",
                "Secure communication"
            },
            CorrectIndex = 1,
            Explanation = "Phishing tricks users into revealing sensitive information."
            },
            new QuizQuestion
            {
            Question = "Which is a secure website?",
            Options = new List<string> { "http://example.com", "https://secure-site.com" },
            CorrectIndex = 1,
            Explanation = "HTTPS websites encrypt your data."
            },
            new QuizQuestion
            {
            Question = "You should use the same password for all accounts. (True/False)",
            Options = new List<string> { "True", "False" },
            CorrectIndex = 1,
            Explanation = "Using unique passwords for each site reduces the risk if one is hacked."
            },
            new QuizQuestion
            {
            Question = "What does 2FA stand for?",
            Options = new List<string> { "Two-Factor Authentication", "Too Fast Action", "Two-Fire Access", "Top File Agreement" },
            CorrectIndex = 0,
            Explanation = "2FA adds a second layer of security during login."
            },
            new QuizQuestion
            {
            Question = "What is a VPN used for?",
            Options = new List<string> { "Gaming", "Speeding up downloads", "Secure online browsing", "Running backups" },
            CorrectIndex = 2,
            Explanation = "VPNs encrypt your internet traffic and hide your location."
            },
            new QuizQuestion
            {
            Question = "Is it safe to click on unknown links in text messages? (Yes/No)",
            Options = new List<string> { "Yes", "No" },
            CorrectIndex = 1,
            Explanation = "No! Links can lead to malware or phishing websites."
            }
        };
        // Method to get a random selection of quiz questions
        public static List<QuizQuestion> GetRandomQuestions(int count = 5)
        {
            return Questions.OrderBy(q => Guid.NewGuid()).Take(count).ToList(); // Randomly selects 'count' questions from the list
        }

        // Static variable to keep track of the user's score
        public static int Score = 0;
    }
}
