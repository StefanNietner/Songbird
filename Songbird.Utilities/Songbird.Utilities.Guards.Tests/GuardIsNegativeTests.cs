using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardIsNegativeTests
    {
        static readonly List<int> _validValues = new List<int>() 
        { 
            -1, 
            -42,
            int.MinValue,
        };
        static readonly List<int> _invalidValues = new List<int>() 
        { 
            0,
            1,
            42,
            int.MaxValue,
        };
        [Test]
        [TestCaseSource(nameof(_invalidValues))]
        public void ShouldThrowIfValueIs(int value)
        {
            Assert.That(() => Guard.IsNegative(value),
                Throws.TypeOf<GuardClauseViolationException>());
        }
        [Test]
        [TestCaseSource(nameof(_validValues))]
        public void ShouldNotThrowExceptionIfValue(int value)
        {
            Assert.That(() => Guard.IsNegative(value),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsViolation()
        {
            Assert.That(() => Guard.IsNegative(1),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : The given value was positive."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.IsNegative(1,"That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }

    }
}
