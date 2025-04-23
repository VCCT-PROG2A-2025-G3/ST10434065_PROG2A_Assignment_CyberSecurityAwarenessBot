# CyberSecurityAwarenessBot
### By: ST10434065 Seth Oliver  
**Group 3 — Programming 2A: Assignment Part 1**

---

## Overview

The **CyberSecurity Awareness Bot** is a console-based C# application designed to engage users in conversations about cyber safety. It answers common cybersecurity-related questions and shares helpful information in a simple, user-friendly way. This project demonstrates key programming skills such as class structuring, method interaction, file handling, and audio integration.

---

## Features

- Custom ASCII logo display with colored output
- Sound effects for startup and welcome messages
- Typing animation for chatbot responses
- User-friendly chatbot that handles free-text input
- Topic recognition for:
  - Phishing
  - Password safety
  - Safe browsing
  - General cybersecurity awareness
- Built-in command to exit the chatbot (`exit` or `quit`)

---

## Project Structure

This application is organized into separate classes to improve maintainability and readability:

- `Program.cs`: The main entry point of the application. Coordinates the flow between logo, audio, and chatbot.
- `LogoClass.cs`: Displays a stylized ASCII logo with alternating colors using `ConsoleColor`.
- `AudioClass.cs`: Plays `.wav` files for startup and welcome sounds.
- `ChatBot.cs`: Core logic of the chatbot, handles user input and generates context-aware responses.

---

## How to Run

1. Clone the repository or download the project files.
2. Open the solution in Visual Studio.
3. Place the required audio files (`Microsoft Windows XP Startup Sound.wav` and `Welcome Message.wav`) in an `Audio` folder located in the same directory as the executable.
4. Run the program.

---

## Sample Interaction
You: hey
Chatbot: Hey! What cyber security questions do you have for me?

You: tell me about phishing
Chatbot: Phishing is when attackers try to trick you into giving personal info via fake emails or messages. Always double check URLs and never click suspicious links.

You: how are you
Chatbot: I'm fully patched and running smooth. Thanks for asking!

You: what can I ask you
Chatbot: You can ask me about phishing, password safety, safe browsing or anything cyber related.

You: exit
Chatbot: Stay safe out there. Goodbye!

---

## Technologies Used

- C# (.NET Framework)
- Console Application
- System.Media for audio playback
- Thread.Sleep for visual and audio timing
- ConsoleColor for visual formatting

---

## Academic Integrity

This project is part of my Programming 2A coursework. Some inspiration and debugging help was sourced through AI tools for learning purposes. All logic, structure, and code design were written and personalized by me.

---

## License

This repository is shared for educational purposes only and is not intended for commercial or unauthorized use. All rights reserved by Seth Oliver.
