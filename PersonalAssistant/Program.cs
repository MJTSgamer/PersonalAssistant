//WHEN ADDING A COMMAND, MAKE SURE TO ADD IT TO THE COMMAND LIST!!

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace PersonalAssistant
{
    internal class Program
    {
        //This is the name the program will call you.
        public static string name = "Sir";
        
        //Speech engines
        private readonly SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        public static SpeechSynthesizer Paige = new SpeechSynthesizer();
        
        //the volume of 'paige'
        public static int PaigeVolume = 100;

        //the Instance of the command system. be aware to only instantiate one, otherwise the commands will not be recognized!
        //I recommend  copying this variable and use it in other files or just reference this variable.
        public static CommandSystem commandSystem;
        

        /// <summary>
        /// This part of the  code will be called first!
        /// </summary>
        private static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
        }

        /// <summary>
        /// you can think of this as the 'start' method in Unity. (those who not know that is basically the method that is called first)
        /// </summary>
        private void Start()
        {
            commandSystem = new CommandSystem();
            commandSystem.Initialize();
            
            startRecognition();

            Paige.SelectVoice("Microsoft Hazel Desktop");
            Paige.Volume = 100;

            Paige.SpeakAsync("Welcome Back " + name);
            
            //prevent the program from closing.
            Console.ReadLine();
        }

        private void startRecognition()
        {
            //load the grammar. read the microsof docs on it here -->(https://docs.microsoft.com/en-us/dotnet/api/System.Speech.Recognition.Grammar.-ctor?view=netframework-4.8#remarks) 
            var gb = new GrammarBuilder(new Choices(commandSystem.AvailableCommands)) {Culture = new CultureInfo("en-GB")};
            var gr = new Grammar(gb) {Name = "DefaultCommands"};

            try
            {
                _recognizer.RequestRecognizerUpdate();
                _recognizer.LoadGrammarCompleted += LoadGrammarCompleted;
                _recognizer.SpeechRecognized += SpeechRecognized;
                _recognizer.LoadGrammarAsync(gr);
                _recognizer.SetInputToDefaultAudioDevice();
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        /// <summary>
        /// This is called when the grammar is loaded. not necessary to use this method.
        /// You can delete this method if you want! along with line 66: "_recognizer.LoadGrammarCompleted += LoadGrammarCompleted;" of course
        /// </summary>
        private void LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            Console.WriteLine("Grammar loaded: " + e.Grammar.Name);
            Console.WriteLine();
        }

        /// <summary>
        /// This method is called when any of the commands are recognized, the confidence level and command self is send, along with some other information.
        /// </summary>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var command = e.Result.Text.ToLower();
            double confidence = e.Result.Confidence;

            Console.WriteLine(e.Result.Text + " - " + e.Result.Confidence);

            commandSystem.InvokeCommand(command, confidence);
        }
    }
}