using NUnit.Framework;
using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Guards.Tests
{
    public class GuardClauseViolationExceptionTests
    {
        [Test]
        public void ParameterlessConstructorShouldSetMessageToDefault()
        {
            Assert.That(new GuardClauseViolationException().Message,
                Is.EqualTo("Guard clause violated."));
        }

        [Test]
        public void GivenMessageShouldBePrefixedWithDefaultMessage()
        {
            Assert.That(new GuardClauseViolationException("Error").Message,
                Does.StartWith("Guard clause violated. : "));
            Assert.That(new GuardClauseViolationException("Error", new System.Exception()).Message,
                Does.StartWith("Guard clause violated. : "));
        }

        [Test]
        public void GivenMessageShouldBeAppendedUnchanged()
        {
            Assert.That(new GuardClauseViolationException("Error").Message,
                Does.EndWith("Error"));
            Assert.That(new GuardClauseViolationException("Error", new System.Exception()).Message,
                Does.EndWith("Error"));
        }
    }
}