using Songbird.Utilities.Guards.Exceptions;
using System;

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
        ///         Guard.NotNull(response, "No Response was given.");
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
        public static void NotNull<T>(T value, string message)
        {
            if (value is null) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// Checks the given string to not be NULL or an empty string
        /// </summary>
        /// <param name="value">The string to be checked</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was NULL or empty.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your name: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.NotNullOrEmpty(entry);
        ///         //from this point on it is assured that entry has a value;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void NotNullOrEmpty(string value)
        {
            NotNullOrEmpty(value, "String was NULL or empty.");
        }
        /// <summary>
        /// Checks the given string to not be NULL or an empty string
        /// </summary>
        /// <param name="value">The string to be checked</param>
        /// <param name="message">The custom error message.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was NULL or empty.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your name: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.NotNullOrEmpty(entry, "Please enter a value next time.");
        ///         //from this point on it is assured that entry has a value;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void NotNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value)) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// Checks the given string to not be NULL or a whitespace string
        /// </summary>
        /// <param name="value">The string to be checked</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was NULL or whitespace.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your name: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.NotNullOrWhitespace(entry);
        ///         //from this point on it is assured that entry has a value;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void NotNullOrWhitespace(string value)
        {
            NotNullOrWhitespace(value, "String was NULL or Whitespace.");
        }
        /// <summary>
        /// Checks the given string to not be NULL or a whitespace string
        /// </summary>
        /// <param name="value">The string to be checked</param>
        /// <param name="message">The custom error message.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was NULL or whitespace.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your name: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.NotNullOrWhitespace(entry, "Please provide a value next time.");
        ///         //from this point on it is assured that entry has a value;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void NotNullOrWhitespace(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// Checks the given value to be convertible to int.
        /// </summary>
        /// <param name="value">The value to be checked</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> could not be converted to int.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(entry);
        ///         //from this point on it is assured that entry is int;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInteger(object value)
        {
            IsInteger(value, "Given value could not be converted to Integer.");
        }
        /// <summary>
        /// Checks the given value to be convertible to int.
        /// </summary>
        /// <param name="value">The value to be checked</param>
        /// <param name="message">The custom error message</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> could not be converted to int.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(entry, "Please provide a valid age.");
        ///         //from this point on it is assured that entry is int;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInteger(object value, string message)
        {
#pragma warning disable IDE0059
            IsInteger(value, message, out int retVal);
#pragma warning restore IDE0059
        }
        /// <summary>
        /// Checks the given value to be convertible to int.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="retVal">The converted value.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> could not be converted to int.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(entry, out int age);
        ///         //from this point on it is assured that age is initialized to the correct value;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInteger(object value, out int retVal)
        {
            IsInteger(value, "Given value could not be converted to Integer.", out retVal);
        }
        /// <summary>
        /// Checks the given value to be convertible to int.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="message">The custom error message</param>
        /// <param name="retVal">The converted value.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> could not be converted to int.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(entry, "Please provide a valid age", out int age);
        ///         //from this point on it is assured that entry is int;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInteger(object value, string message, out int retVal)
        {
            if (!(int.TryParse(value?.ToString() ?? "", out retVal))) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// Checks the given value to be between the given constraints.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="lower">The lower constraint.</param>
        /// <param name="upper">The upper constraint.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was lower than <paramref name="lower"/> or greater than <paramref name="upper"/></exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter a year: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(value, out int year);
        ///         Guard.IsInRange(year, 1900, 2100);
        ///         //from this point on it is assured that year is less than 1900 and greater than 2100;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInRange(int value, int lower, int upper)
        {
            IsInRange(value, lower, upper, "The given value was outside the given range.");
        }
        /// <summary>
        /// Checks the given value to be between the given constraints.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="lower">The lower constraint.</param>
        /// <param name="upper">The upper constraint.</param>
        /// <param name="message">The custom error message</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> was lower than <paramref name="lower"/> or greater than <paramref name="upper"/></exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter a year: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(value, "Please enter a valid year.", out int year);
        ///         Guard.IsInRange(year, 1900, 2100);
        ///         //from this point on it is assured that year is less than 1900 and greater than 2100;
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsInRange(int value, int lower, int upper, string message)
        {
            if(value > upper || value < lower) throw new GuardClauseViolationException(message);
        }

        /// <summary>
        /// Checks the given <paramref name="value"/> to be positive, throwing an Exception if it is not. 
        /// </summary>
        /// <param name="value">
        /// The value to be checked.
        /// </param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> is negative.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(value, out int age);
        ///         Guard.IsPositive(age);
        ///         //from this point on it is assured that age is initialized with a valid value
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsPositive(int value)
        {
            IsPositive(value, "The given value was negative.");
        }
        /// <summary>
        /// Checks the given <paramref name="value"/> to be positive, throwing an Exception if it is not. 
        /// </summary>
        /// <param name="value">
        /// The value to be checked.
        /// </param>
        /// <param name="message">The given error message.</param>
        /// <exception cref="GuardClauseViolationException"><paramref name="value"/> is negative.</exception>
        /// <example>
        /// <code>
        /// //param coming from outside source
        /// public static void Main(string[] args)
        /// {
        ///     Console.Write("Enter your age: ");
        ///     try
        ///     {
        ///         string entry = Console.ReadLine();
        ///         Guard.IsInteger(value, "Please enter a valid age.", out int age);
        ///         Guard.IsPositive(age);
        ///         //from this point on it is assured that age is initialized with a valid value
        ///     }
        ///     catch(GuardClauseViolationException ex)
        ///     {
        ///         //ErrorHandling here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void IsPositive(int value, string message)
        {
            if (value < 0) throw new GuardClauseViolationException(message);
        }
        //is positive
        //is negative
        //is zero
        //is less than
        //is greater than
    }
}
