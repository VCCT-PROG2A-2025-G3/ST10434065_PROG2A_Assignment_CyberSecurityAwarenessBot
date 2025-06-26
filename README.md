# CyberSecurityAwarenessBot  
### By: ST10434065 Seth Oliver  
**Group 3 — Programming 2A: Assignment POE**

---

## Overview

**CyberSecurity Awareness Bot** is an interactive C# **Windows Forms Application** designed to help users learn about cybersecurity through a smart, conversational chatbot. The application supports natural language processing (NLP)-style inputs, quiz-based learning, task and reminder management, and sentiment-aware responses. It demonstrates modular class design, collections, delegates, conditional logic, and rich GUI functionality.

---

## Key Features

### Chatbot Interaction
- Smart chatbot with **free-text understanding**
- Responds to keywords like:
  - **Phishing**, **Password Safety**, **Safe Browsing**, **Privacy**, **Firewalls**, **Malware**, and more
- Detects **sentiments** like *worried*, *curious*, *confused*, *happy*, and responds empathetically
- Offers **follow-up responses** and remembers recent topics
- Built-in command:  
  - `"What can I learn from you"` → shows all supported topics
  - `"Show activity log"` or `"What have I done"` → shows chatbot action history

---

### Task & Reminder Management
- **Natural Language Commands** like:
  - `"Add task"` / `"Add a task"`  
  - `"Set a reminder"` or `"Remind me at yyyy-MM-dd HH:mm"`
- GUI for:
  - Task creation with optional reminder
  - View, remove, mark done, and clear tasks
- All task actions are **logged** in the Activity Log

---

### Quiz Feature
- Built-in multiple-choice **CyberSecurity Quiz**
- User clicks **“Start Quiz”** to launch
- Score is tracked and displayed after submission
- Activity Log records each quiz session and final score

---

### Activity Log
- Tracks chatbot actions such as:
  - Task additions
  - Reminder setups
  - Quiz attempts and completions
  - Keyword/topic responses
- Type `"Show activity log"` to see last 10 actions

---

##  GUI Features

- Custom **startup sound** and **welcome message**
- Stylish **ASCII logo** on launch
- **Chat-style interface** (You ↔ Bot)
- List box displaying tasks with reminders
- Buttons to:
  - Add, remove, mark complete, or clear tasks
  - Start the cybersecurity quiz
- Dynamic response area with automatic scrolling and colored bot responses

---

##  Project Structure

The project is divided into the following classes for maintainability:

| Class Name              | Responsibility |
|------------------------|----------------|
| `MainForm.cs`          | Handles all WinForms GUI logic and interaction with the chatbot |
| `ChatBotWinFormsWrapper.cs` | Core logic for chatbot conversation and processing |
| `TaskItem.cs`          | Represents an individual task (title, description, reminder) |
| `TaskManager.cs`       | Manages all task storage and manipulation |
| `ResponseClass.cs`     | Stores topic responses and unknown input replies |
| `AudioClass.cs`        | Manages audio playback for startup and welcome |
| `QuizForm.cs`          | Displays and handles the cybersecurity quiz UI |

---

##  Sample Interactions

**User:** add task  
**Bot:** What is the title of the task you'd like to add?  
**User:** Update antivirus  
**Bot:** Now, what is the description of Update antivirus?  
**User:** Weekly virus scan  
**Bot:** Great. Would you like to set a reminder? (yes/no)  

---

**User:** tell me about malware  
**Bot:** Don’t download cracked software – it’s often infected.  
Let me know if i should explain or elaborate more on this topic.

---

**User:** show activity log  
**Bot:**  
Here’s a summary of recent actions:  
1. Task added: ‘Update antivirus’  
2. Reminder set for 2025-06-28 15:00  
3. Quiz completed with a score of 4/5  

---

## How to Run the App

1. Open the `.sln` file in **Visual Studio**
2. Ensure the audio files (`Microsoft Windows XP Startup Sound.wav`, `Welcome Message.wav`) are in a folder named `Audio` next to the executable
3. Press `F5` or click **Start** to run the application

---

## Technologies Used

- C# (.NET WinForms)
- Lists, Queues, Dictionaries
- Delegates and Collections
- `System.Media` for audio
- `Regex` for natural language date/time parsing
- GUI Events, Forms, Controls (ListBox, Buttons, TextBox)

---

## Academic Integrity

This is a final POE assignment for Programming 2A at Varsity College, completed by **Seth Oliver (ST10434065)**.  
All core logic, GUI design, interaction logic, and integration were written by me. Any use of AI tools was for **debugging and learning purposes only**.

---

## License

This project is for educational use only. All rights reserved © 2025 by Seth Oliver.

