using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Utilities.Guards
{
    public static class Guard
    {
        public static void NotNull(object value)
        {
            if (value is null) throw new GuardClauseViolationException("Null value given.");
        }
        public static void NotNullOrEmpty(string value)
        {

        }
        public static void NotNullOrWhitespace(string value)
        {

        }
    }
}
