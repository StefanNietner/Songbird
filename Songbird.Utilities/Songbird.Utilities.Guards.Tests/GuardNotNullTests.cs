using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;

namespace Songbird.Guards.Tests
{
    public class GuardNotNullTests
    {
        [Test]
        public void ShouldThrowGuardClauseViolatedExceptionIfValueIsNull()
        {
            Assert.That( () => Guard.NotNull(null), 
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
            Assert.That(() => Guard.NotNull(null),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : Null value given."));
        }
    }
}
