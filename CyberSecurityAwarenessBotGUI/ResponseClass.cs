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

namespace CyberSecurityAwarenessBot
{   
    // This class contains the responses for various topics related to cybersecurity awareness.
    class ResponseClass
    {
        #region Dictionary of Responses
        // This dictionary maps specific topics to a list of responses that can be provided to the user.
        public static Dictionary<string, List<string>> TopicResponses = new Dictionary<string, List<string>>()
        {
            ["phishing"] = new List<string>()
            {
                "Phishing scams pretend to be trustworthy emails. Always check links before clicking.",
                "Look for grammar mistakes or urgency – signs of phishing.",
                "Don't trust unexpected email attachments.",
                "Use spam filters and two-factor authentication to protect yourself from phishing.",
                "Report phishing attempts to your IT department.",
                "Always verify sender addresses closely; a single letter can be off.",
                "Be wary of emails requesting urgent money transfers.",
                "Hover over links to preview the actual destination.",
                "Legitimate companies don’t ask for sensitive data via email.",
                "If unsure, call the organization directly using a trusted number."
            },
            ["password"] = new List<string>()
            {
                "Use a mix of letters, numbers, and symbols in your password.",
                "Avoid using the same password on multiple accounts.",
                "Use a password manager to store complex passwords.",
                "Enable two-factor authentication for added protection.",
                "Never share your passwords with anyone.",
                "Change your passwords regularly, especially for sensitive accounts.",
                "Avoid using personal info like birthdates or pet names.",
                "Don’t write your passwords down on sticky notes.",
                "Longer passwords are more secure – aim for at least 12 characters.",
                "Use passphrases: a sentence that's easy to remember but hard to crack."
            },
            ["safe browsing"] = new List<string>()
            {
                "Use HTTPS websites to ensure secure connections.",
                "Avoid clicking unknown pop-ups or ads.",
                "Keep your browser updated to avoid vulnerabilities.",
                "Use incognito mode when browsing sensitive content.",
                "Install a reputable antivirus tool.",
                "Don’t save passwords in your browser unless it’s secure.",
                "Avoid public Wi-Fi when entering private data.",
                "Check for the padlock icon next to the URL.",
                "Use content blockers to avoid malicious scripts.",
                "Clear your browsing history and cookies regularly."
            },
            ["scam"] = new List<string>()
            {
                "Online scams often involve fake prizes or threats – be cautious.",
                "Never give out personal info unless you’re 100% sure of the source.",
                "Scammers often impersonate trusted brands or government.",
                "If it sounds too good to be true, it probably is.",
                "Verify offers through official channels before acting.",
                "Don’t click on links from unknown messages or texts.",
                "Use reverse image searches to check fake product listings.",
                "Trust your gut — if something feels off, investigate.",
                "Watch for spelling errors in scam messages.",
                "Always do research before engaging with unfamiliar businesses."
            },
            ["privacy"] = new List<string>()
            {
                "Always read the privacy policy before agreeing to it.",
                "Don’t overshare personal information on social media.",
                "Use end-to-end encrypted messaging apps.",
                "Disable location access unless absolutely necessary.",
                "Review app permissions regularly.",
                "Use privacy-focused browsers like Brave or Firefox.",
                "Avoid logging into websites using your social media accounts.",
                "Clear your data history and limit data collection settings.",
                "Cover your webcam when not in use.",
                "Avoid using public profiles unless needed."
            },
            ["malware"] = new List<string>()
            {
                "Malware can come from email attachments, fake websites, or USBs.",
                "Use updated antivirus software to detect malware.",
                "Don’t download cracked software – it’s often infected.",
                "Run regular security scans.",
                "Back up your data regularly to protect against malware damage.",
                "Avoid clicking strange links, even if they come from friends.",
                "Look for unexpected slowdowns – a sign of infection.",
                "Use sandboxed environments for suspicious files.",
                "Keep your operating system updated.",
                "Avoid pirated software – it's a common malware source."
            },
            ["firewall"] = new List<string>()
            {       
                "Firewalls block unauthorized access to your device.",
                "Use both hardware and software firewalls if possible.",
                "Don’t turn off your firewall unless instructed by IT.",
                "Firewalls are your first defense layer.",
                "Always keep your firewall updated.",
                "A firewall monitors incoming and outgoing traffic.",
                "Use network segmentation along with firewalls for better security.",
                "Check firewall logs for suspicious activity.",
                "Configure rules to restrict risky ports.",
                "Never rely solely on firewalls – layer your defenses."
            },
            ["2fa"] = new List<string>()
            {
                "2FA adds an extra layer of protection to your accounts.",
                "Use authenticator apps like Google Authenticator or Authy.",
                "Avoid SMS 2FA if you can – it’s more vulnerable than apps.",
                "Always enable 2FA on email and financial accounts.",
                "Even if your password is compromised, 2FA helps protect access.",
                "Don’t share your 2FA codes with anyone.",
                "Some platforms let you use hardware keys like YubiKey.",
                "You can often set up backup 2FA methods in case you lose your device.",
                "Scan your backup codes and store them securely.",
                "2FA is a simple but powerful step toward online safety."
            },
            ["vpn"] = new List<string>()
            {
                "A VPN encrypts your internet traffic for privacy and security.",
                "Use VPNs when connected to public Wi-Fi.",
                "VPNs help protect your location and identity online.",
                "Free VPNs can be risky – choose a reputable provider.",
                "VPNs can help access region-locked content securely.",
                "Always verify your VPN is connected before browsing sensitive sites.",
                "VPNs do not make you anonymous, but they enhance privacy.",
                "Use a kill switch in your VPN settings to avoid leaks.",
                "Avoid using VPNs for illegal activity – it’s still traceable.",
                "Your ISP cannot see your online activity when using a VPN."
            },
            ["cyberbullying"] = new List<string>()
            {
                "Cyberbullying includes harassment, threats, and shaming online.",
                "Never respond to online bullies – block and report them.",
                "Keep evidence of bullying messages or posts.",
                "Report serious threats to local authorities.",
                "Social media platforms have tools to report abuse.",
                "Talk to a trusted adult or professional if you're affected.",
                "Don’t forward or share bullying messages.",
                "Support others who may be victims of online abuse.",
                "Use privacy settings to control who can message or tag you.",
                "Remember: silence is not weakness – it’s safety."
            }
        };
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Unknown Input Responses
        // This list contains responses for when the user input does not match any known topics.
        public static List<string> UnknownInputResponses = new List<string>()
        {
            "Could you rephrase that in a cybersecurity context?",
            "Interesting... Can you ask something else about online safety?",
            "I'm not sure how that relates to cybersecurity, but I'm here to help!",
            "Try asking me about topics like phishing, passwords, safe browsing, scam, privacy, malware, or firewall."
        };
        #endregion
    }
}



