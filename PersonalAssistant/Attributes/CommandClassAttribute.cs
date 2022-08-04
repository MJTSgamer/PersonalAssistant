using System;

namespace PersonalAssistant
{
    /// <summary>
    ///     Marks that class as a class that may contain commands.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandClassAttribute : Attribute
    {
        
    }
}