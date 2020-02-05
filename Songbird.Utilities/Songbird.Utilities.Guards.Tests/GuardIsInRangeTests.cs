using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardIsInRangeTests
    {
        static readonly List<(int, int, int)> _invalidValues = new List<(int, int, int)>() 
        { 
            (0, 1, 10), 
            (10, 0, 9) 
        };
        static readonly List<(int, int, int)> _validValues = new List<(int, int, int)>() 
        { 
            (0,0,9), 
            (5,1,10), 
            (9,0,9)
        };
        [Test]
        [TestCaseSource(nameof(_invalidValues))]
        public void ShouldThrowIfValueIs((int value, int lower, int upper) val)
        {
            Assert.That(() => Guard.IsInRange(val.value, val.lower, val.upper),
                Throws.TypeOf<GuardClauseViolationException>());
        }
        [Test]
        [TestCaseSource(nameof(_validValues))]
        public void ShouldNotThrowExceptionIfValue((int value, int lower, int upper) val)
        {
            Assert.That(() => Guard.IsInRange(val.value, val.lower, val.upper),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsViolation()
        {
            Assert.That(() => Guard.IsInRange(0, 1, 10),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : The given value was outside the given range."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.IsInRange(0, 1, 10,"That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }

    }
}
