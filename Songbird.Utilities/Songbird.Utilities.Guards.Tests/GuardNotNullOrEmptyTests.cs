using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Guards.Tests
{
    public class GuardNotNullOrEmptyTests
    {
        [Test]
        public void ShouldThrowGuardClauseViolatedExceptionIfValueIsNull()
        {
            string nullObject = null;
            Assert.That( () => Guard.NotNullOrEmpty(nullObject), 
                Throws.TypeOf<GuardClauseViolationException>());
        }
        [Test]
        public void ShouldThrowGuardClauseViolatedExceptionIfValueIsEmpty()
        {
            Assert.That(() => Guard.NotNullOrEmpty(""),
                Throws.TypeOf<GuardClauseViolationException>());
        }

        [Test]
        public void ShouldNotThrowExceptionIfValueNotNull()
        {
            Assert.That(() => Guard.NotNullOrEmpty("Test"),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsNullValueOnViolation()
        {
            Assert.That(() => Guard.NotNullOrEmpty(""),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : String was NULL or empty."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.NotNullOrEmpty("", "That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }
    }
}
