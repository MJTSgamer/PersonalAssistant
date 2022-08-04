using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PersonalAssistant
{
    public class CommandSystem
    {
        MethodInfo[] methods;

        public string[] AvailableCommands = Array.Empty<string>();

        ConfidenceAttribute fallbackConfidenceAttribute;

        public void Initialize()
        {
            var type = GetTypesWitAttribute(typeof(CommandClassAttribute).Assembly);
            
            methods = type.GetType().Assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                .ToArray();

            foreach (MethodInfo method in methods)
            {
                CommandAttribute attr =
                    (CommandAttribute) method.GetCustomAttributes(typeof(CommandAttribute), false).FirstOrDefault();

                for (int i = 0; i < attr.Commands.Length; i++)
                {
                    AvailableCommands = AvailableCommands.Append(attr.Commands[i]).ToArray();
                }
            }

            Console.WriteLine($"{methods.Length} commands loaded in {type.Count()} classes");


            fallbackConfidenceAttribute = new ConfidenceAttribute();
            fallbackConfidenceAttribute.Confidence = 0.75;
        }

        static IEnumerable<Type> GetTypesWitAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(CommandClassAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }
        
        
        /// <summary>
        /// You can call this method when you want to invoke a command.
        /// </summary>
        /// <param name="CommandName">The command recognized in the form of a string</param>
        /// <param name="Confidence">the confidence that the speech engine had it recognized it correctly</param>
        /// <example>
        /// InvokeCommand("help", 0.7654);
        /// </example>
        public void InvokeCommand(string CommandName, double Confidence)
        {
            
            foreach (MethodInfo method in methods)
            {
                CommandAttribute commandAttribute = (CommandAttribute) method.GetCustomAttributes(typeof(CommandAttribute), false).FirstOrDefault();
                ConfidenceAttribute confidenceAttribute = (ConfidenceAttribute) method.GetCustomAttributes(typeof(ConfidenceAttribute), false).FirstOrDefault() ?? fallbackConfidenceAttribute;
                
                for (int i = 0; i < commandAttribute.Commands.Length; i++)
                {
                    if (commandAttribute.Commands[i].ToLower() == CommandName.ToLower() && confidenceAttribute.Confidence <= Confidence)
                    {
                        var commandClass = method.DeclaringType;
                        var commandClassInstance = Activator.CreateInstance(commandClass);
                        
                        method.Invoke(commandClassInstance, null);
                        return;
                    }
                }
            }
        }
    }
}