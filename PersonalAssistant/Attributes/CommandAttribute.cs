using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalAssistant
{
    /// <summary>
    ///     Marks the execution information for a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        ///     Gets the text that has been set to be recognized as a command.
        /// </summary>
        public string[] Commands { get; }
        /// <inheritdoc />
        public CommandAttribute()
        {
            Commands = null;
        }
        
        /// <summary>
        ///     Initializes a new <see cref="CommandAttribute" /> attribute with the specified name.
        /// </summary>
        /// <param name="Commands">The command(s) that the speech recognition engine has to listen for</param>
        public CommandAttribute(params string[] Commands)
        {
            this.Commands = Commands;
        }
    }
}