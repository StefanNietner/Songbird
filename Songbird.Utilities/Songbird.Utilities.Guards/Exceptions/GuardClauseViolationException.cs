using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Songbird.Utilities.Guards.Exceptions
{
    public class GuardClauseViolationException : Exception
    {
        private const string clauseViolatedMessage = "Guard clause violated.";
        public GuardClauseViolationException() 
            : base(clauseViolatedMessage)
        {
        }

        public GuardClauseViolationException(string message) 
            : base (SetMessageWithPrefix(message))
        {
        }

        public GuardClauseViolationException(string message, Exception innerException) 
            : base(SetMessageWithPrefix(message), innerException)
        {
        }

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
