using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Utilities.Guards
{
    /// <summary>
    /// Static class for easy to use common guard clauses
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Checks the given value against NULL, throwing an Exception if the given value is NULL. 
        /// </summary>
        /// <typeparam name="T">
        /// The type of the given value.
        /// </typeparam>
        /// <param name="value">
        /// The value to be checked.
        /// </param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> is NULL</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public void Foo(HttpResponseMessage response)
        /// {
        ///     try
        ///     {
        ///         Guard.NotNull(response);
        ///         //response is assured to not be null at this point.
        ///         //so it can be processed freely
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //Errorhandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        /// Some remarkable information here
        /// maybe somethings in a second line?
        /// </remarks>
        public static void NotNull<T>(T value)
        {
            NotNull(value, "Null value given.");
        }
        /// <summary>
        /// Checks the given value against NULL and throws an <see cref="GuardClauseViolationException"/> if true. 
        /// </summary>
        /// <typeparam name="T">The type of the given value.</typeparam>
        /// <param name="value">The value to be checked.</param>
        /// <param name="message">The custom error message.</param>
        /// <exception cref="GuardClauseViolationException">Occurs when the given <paramref name="value"/> is NULL</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public void Foo(<see cref="System.Net.Http.HttpResponseMessage"/> response)
        /// {
        ///     try
        ///     {
        ///         <see cref="Guard"/>.NotNull(response, "No Response was given.");
        ///         //response is assured to not be null at this point.
        ///         //so it can be processed freely
        ///     }
        ///     catch(<see cref="GuardClauseViolationException"/> ex)
        ///     {
        ///         //Errorhandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void NotNull<T>(T value, string message)
        {
            if (value is null) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>A test string</returns>
        public static string Test(string s)
        {
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void NotNullOrEmpty(string value)
        {
            NotNullOrEmpty(value, "String was NULL or empty.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
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
#pragma warning disable IDE0059
            IsInteger(value, message, out int retVal);
#pragma warning restore IDE0059
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
        //is positive
        //is negative
        //is zero
        //is less than
        //is greater than
    }

    /// <summary>
    /// 
    /// </summary>
    public class SecondClass
    {
        /// <summary>
        /// 
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void TestMethod()
        {

        }
    }
}
