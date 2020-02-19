using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Guards.Tests
{
    public class GuardNotNullTests
    {
        [Test]
        public void ShouldThrowGuardClauseViolatedException()
        {
            object nullObject = null;
            Assert.That( () => Guard.NotNull(nullObject), 
                Throws.TypeOf<GuardClauseViolationException>());
        }

        [Test]
        public void ShouldNotThrowExceptionIfValueNotNull()
        {
            Assert.That(() => Guard.NotNull(new { }),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsNullValueOnViolation()
        {
            object nullObject = null;
            Assert.That(() => Guard.NotNull(nullObject),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : Null value given."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            object nullObject = null;
            Assert.That(() => Guard.NotNull(nullObject, "That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }
    }
}
