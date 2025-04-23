/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT PART 1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CyberSecurityAwarenessBot
{
    // This class is responsible for playing audio files.
    class AudioClass
    {
        // This method plays the startup sound.
        public static void PlayStartupSound()
        {
            PlayAudio("Microsoft Windows XP Startup Sound.wav"); // The startup sound
        }

        //--------------------------------------------------------------------------------------------------------------//

        // This method plays the welcome message.
        public static void PlayWelcomeMessage()
        {
            PlayAudio("Welcome Message.wav"); // The welcome message
        }

        //--------------------------------------------------------------------------------------------------------------//

        // This method plays the specified audio file.
        private static void PlayAudio(string name)
        {
            // Construct the full path to the audio file
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", name);
            // Check if the file exists before trying to play it
            if (File.Exists(path))
            {
                using (SoundPlayer player = new SoundPlayer(path)) // Create a new SoundPlayer instance 
                {
                    player.PlaySync(); // PlaySync to wait for the sound to finish
                }
            }
            // If the file does not exist, print a message to the console
            else
            {
                Console.WriteLine($"Audio file {name} not found."); // Print an error message
            }
        }
    }
}
