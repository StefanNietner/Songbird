using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Utilities.Guards
{
    public static class Guard
    {
        public static void NotNull<T>(T value)
        {
            NotNull(value, "Null value given.");
        }
        public static void NotNull<T>(T value, string message)
        {
            if (value is null) throw new GuardClauseViolationException(message);
        }

        public static void NotNullOrEmpty(string value)
        {
            NotNullOrEmpty(value, "String was NULL or empty.");
        }
        public static void NotNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value)) throw new GuardClauseViolationException(message);
        }

        public static void NotNullOrWhitespace(string value)
        {
            NotNullOrWhitespace(value, "String was NULL or Whitespace.");
        }
        public static void NotNullOrWhitespace(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new GuardClauseViolationException(message);
        }

        public static void IsInteger(object value)
        {
            IsInteger(value, "Given value could not be converted to Integer.");
        }
        public static void IsInteger(object value, string message)
        {
            IsInteger(value, message, out int retVal);
        }
        public static void IsInteger(object value, out int retVal)
        {
            IsInteger(value, "Given value could not be converted to Integer.", out retVal);
        }
        public static void IsInteger(object value, string message, out int retVal)
        {
            if (!(int.TryParse(value?.ToString() ?? "", out retVal))) throw new GuardClauseViolationException(message);
        }
        
        public static void IsInRange(int value, int lower, int upper)
        {
            IsInRange(value, lower, upper, "The given value was outside the given range.");
        }
        public static void IsInRange(int value, int lower, int upper, string message)
        {
            if(value > upper || value < lower) throw new GuardClauseViolationException(message);
        }
        //is in range
        //is positive
        //is negative
        //is zero
        //is less than
        //is greater than
    }
}
