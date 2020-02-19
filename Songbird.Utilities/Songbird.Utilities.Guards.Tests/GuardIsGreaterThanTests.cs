using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardIsGreaterThanTests
    {
        static readonly List<(int, int)> _invalidValues = new List<(int, int)>()
        {
            (0,            0),
            (-1,           1),
            (-42,          -10),
            (int.MinValue, 1),
        };
        static readonly List<(int, int)> _validValues = new List<(int, int)>()
        {
            (1,            0),
            (42,           10),
            (int.MaxValue, 10),
        };
        [Test]
        [TestCaseSource(nameof(_invalidValues))]
        public void ShouldThrowIfValueIs((int value, int limit)value)
        {
            Assert.That(() => Guard.IsGreaterThan(value.value, value.limit),
                Throws.TypeOf<GuardClauseViolationException>());
        }
        [Test]
        [TestCaseSource(nameof(_validValues))]
        public void ShouldNotThrowExceptionIfValue((int value, int limit) value)
        {
            Assert.That(() => Guard.IsGreaterThan(value.value, value.limit),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsViolation()
        {
            Assert.That(() => Guard.IsGreaterThan(0, 1),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : The given value was less or equal to the given limit."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.IsGreaterThan(0,1,"That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }

    }
}
