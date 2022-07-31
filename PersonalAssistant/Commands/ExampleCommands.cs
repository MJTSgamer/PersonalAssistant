using System;
using System.Diagnostics;
using System.IO;
using System.Speech.Synthesis;

namespace PersonalAssistant
{
    public class ExampleCommands
    {
        //--<- Variables ->--//
        private SpeechSynthesizer Paige = Program.Paige;
        private string Username = Program.name;
        private int PaigeVolume = Program.PaigeVolume;
        private PAUtilities utils = new PAUtilities();
        
        //--<- Commands ->--//
        
        
        [Command("Hello", "hi", "hey")]
        [Confidence(90)]
        public void Hello()
        {
            Random random = new Random();
            int rand = random.Next(0, 3);
            if (rand == 0)
            {
                Paige.SpeakAsync("Hey there, How can i help you today?");
            }                 

            else if (rand == 1)
                Paige.SpeakAsync("Hey, Wonderful day isn't it ");
            else
                Paige.SpeakAsync("Yes, what can i do for you?");
        }

        
        [Command("open google", "open chrome")]
        [Confidence(70)]
        public void OpenOperaWithGoogle()
        {
            Paige.SpeakAsync("Opening Opera GX with google open " + Username);
                    
            //open browser
            utils.OpenUrl("https://google.com");
        }
        
        
        [Command("open youtube", "open youtube")]
        [Confidence(70)]
        public void OpenYoutube()
        {
            Paige.SpeakAsync("Opening Youtube " + Username);
                    
            // Open Youtube
            utils.OpenUrl("https://youtube.com");
        }
        
        
        [Command("open github")]
        [Confidence(60)]
        public void OpenGithub()
        {
            Paige.SpeakAsync("Opening Github " + Username);
                    
            utils.OpenUrl("https://github.com");
        }


        [Command("mute")]
        [Confidence(70)]
        public void Mute()
        {
            PaigeVolume = 50;
            Paige.Speak("okay, just say, unmute, to unmute me.");
                    
            PaigeVolume = 0;
            Paige.Volume = PaigeVolume;
        }
        
        
        [Command("unmute")]
        [Confidence(70)]
        public void Unmute()
        {
            Paige.SpeakAsync("Happy to hear that.");
                    
            PaigeVolume = 100;
            Paige.Volume = PaigeVolume;
        }
        
        
        [Command("Weather", "current weather", "what is the weather", "what is the current weather")]
        [Confidence(90)]
        public void Weather()
        {
            Paige.SpeakAsync("Here is the weather for today " + Username);
            utils.OpenUrl(@"https://www.google.com/search?q=current+weather");
        }
        
        
        [Command("Shut off", "turn off", "off", "shut down")]
        [Confidence(90)]
        public void ShutOff()
        {
            Paige.Speak("Shutting down, bye " + Username);
            Environment.Exit(0);
        }
        
        
        [Command("help me", "what are the commands", "what are your commands")]
        [Confidence(50)]
        public void Help()
        {
            Paige.SpeakAsync("Here are the commands I can do " + Username);
            
            //File and path you want to create and write to
            string fileName = Environment.CurrentDirectory + @"\Commands.txt";
            //Check if the file exists

            CommandSystem cs = Program.commandSystem;

            StreamWriter writer = new StreamWriter(fileName);

            foreach (string command in cs.AvailableCommands)
            {
                writer.WriteLine(command);
            }

            writer.Close();
            
            Process.Start("notepad.exe", fileName);
        }

        [Command("close help me")]
        [Confidence(70)]
        public void CloseHelp()
        {
            Paige.SpeakAsync("Sure, closing help list " + Username);
            utils.CloseApplication("notepad", true);
        }
    }
}