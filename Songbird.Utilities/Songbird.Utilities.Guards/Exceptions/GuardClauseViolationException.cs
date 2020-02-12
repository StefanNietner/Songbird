using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Songbird.Utilities.Guards.Exceptions
{
    /// <summary>
    /// Exception to indicate a failed guard clause.
    /// </summary>
    public class GuardClauseViolationException : Exception
    {
        private const string clauseViolatedMessage = "Guard clause violated.";
        /// <summary>
        /// Instantiates a new instance of the GuardClauseViolationException class with a default error message.
        /// </summary>
        public GuardClauseViolationException() 
            : base(clauseViolatedMessage)
        {
        }
        /// <summary>
        /// Instantiates a new instance of the GuardClauseViolationException class with a specified error message.
        /// </summary>
        /// <param name="message">The specified error message</param>
        public GuardClauseViolationException(string message) 
            : base (SetMessageWithPrefix(message))
        {
        }
        /// <summary>
        /// Instantiates a new instance of the GuardClauseViolationException class with a specified error message and a reference to the inner exception that caused this.
        /// </summary>
        /// <param name="message">The specified error message</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GuardClauseViolationException(string message, Exception innerException) 
            : base(SetMessageWithPrefix(message), innerException)
        {
        }
        /// <summary>
        /// Initializes a new instance of the GuardClauseViolationException class with serialized data
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException"><paramref name="info"/> is null.</exception>
        /// <exception cref="SerializationException">The class name is null or System.Exception.HResult is zero (0)</exception>
        protected GuardClauseViolationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        private static string SetMessageWithPrefix(string message)
        {
            
            return $"{clauseViolatedMessage} : {message}";
        }
    }
}
