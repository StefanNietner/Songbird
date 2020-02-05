using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardNotNullOrWhitespaceTests
    {
        static readonly List<string> _invalidValues = new List<string>() { null, "", " ", "\t", "\r", "\n", "\r\n" };
        [Test]
        [TestCaseSource("_invalidValues")]
        public void ShouldThrowIfValueIs(string val)
        {
            Assert.That(() => Guard.NotNullOrWhitespace(val),
                Throws.TypeOf<GuardClauseViolationException>());
        }

        [Test]
        public void ShouldNotThrowExceptionIfValueNotNull()
        {
            Assert.That(() => Guard.NotNullOrWhitespace("Test"),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsNullValueOnViolation()
        {
            Assert.That(() => Guard.NotNullOrWhitespace(""),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : String was NULL or Whitespace."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.NotNullOrWhitespace("", "That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }
    }
}
