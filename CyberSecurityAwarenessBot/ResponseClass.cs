/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT PART 1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{   // This class contains the responses for various topics related to cybersecurity awareness.
    class ResponseClass
    {   // This dictionary maps specific topics to a list of responses that can be provided to the user.
        public static Dictionary<string, List<string>> TopicResponses = new Dictionary<string, List<string>>()
        {
            ["phishing"] = new List<string>() // Responses related to phishing
            {
                "Phishing scams pretend to be trustworthy emails. Always check links before clicking.",
                "Look for grammar mistakes or urgency – signs of phishing.",
                "Don't trust unexpected email attachments.",
                "Use spam filters and two-factor authentication to protect yourself from phishing.",
                "Report phishing attempts to your IT department."
            },
            ["password"] = new List<string>() // Responses related to password security
            {
                "Use a mix of letters, numbers, and symbols in your password.",
                "Avoid using the same password on multiple accounts.",
                "Use a password manager to store complex passwords.",
                "Enable two-factor authentication for added protection.",
                "Never share your passwords with anyone."
            },
            ["safe browsing"] = new List<string>() // Responses related to safe browsing practices
            {
                "Use HTTPS websites to ensure secure connections.",
                "Avoid clicking unknown pop-ups or ads.",
                "Keep your browser updated to avoid vulnerabilities.",
                "Use incognito mode when browsing sensitive content.",
                "Install a reputable antivirus tool."
            },
            ["scam"] = new List<string>() // Responses related to online scams
            {
                "Online scams often involve fake prizes or threats – be cautious.",
                "Never give out personal info unless you’re 100% sure of the source.",
                "Scammers often impersonate trusted brands or government.",
                "If it sounds too good to be true, it probably is.",
                "Verify offers through official channels before acting."
            },
            ["privacy"] = new List<string>() // Responses related to online privacy
            {
                "Always read the privacy policy before agreeing to it.",
                "Don’t overshare personal information on social media.",
                "Use end-to-end encrypted messaging apps.",
                "Disable location access unless absolutely necessary.",
                "Review app permissions regularly."
            },
            ["malware"] = new List<string>() // Responses related to malware
            {
                "Malware can come from email attachments, fake websites, or USBs.",
                "Use updated antivirus software to detect malware.",
                "Don’t download cracked software – it’s often infected.",
                "Run regular security scans.",
                "Back up your data regularly to protect against malware damage."
            },
            ["firewall"] = new List<string>() // Response related to firewalls
            {
                "Firewalls block unauthorized access to your device.",
                "Use both hardware and software firewalls if possible.",
                "Don’t turn off your firewall unless instructed by IT.",
                "Firewalls are your first defense layer.",
                "Always keep your firewall updated."
            }
        };
        // This list contains responses for when the user input does not match any known topics.
        public static List<string> UnknownInputResponses = new List<string>()
        {
            "Could you rephrase that in a cybersecurity context?",
            "Interesting... Can you ask something else about online safety?",
            "I'm not sure how that relates to cybersecurity, but I'm here to help!",
            "Try asking me about topics like phishing, passwords, safe browsing, scam, privacy, malware, or firewall."
        };
    }
}



