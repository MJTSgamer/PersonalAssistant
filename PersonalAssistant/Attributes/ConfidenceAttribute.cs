using System;

namespace PersonalAssistant
{
    /// <summary>
    ///     Marks the confidence that the speech recognition engine has to have in the accuracy of the recognized text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConfidenceAttribute : Attribute
    {
        /// <summary>
        ///     Gets the confidence level.
        /// </summary>
        public double Confidence { get; set; }

        /// <inheritdoc />
        public ConfidenceAttribute()
        {
            Confidence = 0.5;
        }

        /// <summary>
        ///     Initializes a new <see cref="ConfidenceAttribute" /> attribute with the specified name.
        /// </summary>
        /// <param name="confidence">The minimum confidence that the speech recognition engine has to have in the accuracy of the recognized text. between 0-100</param>
        public ConfidenceAttribute(int confidence)
        {
            this.Confidence = (double)confidence / 100;
        }
    }
}