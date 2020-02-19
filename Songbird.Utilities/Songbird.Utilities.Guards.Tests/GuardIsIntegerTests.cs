using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardIsIntegerTests
    {
        static readonly List<object> _invalidValues = new List<object>() 
        { 
            null, 
            "", 
            1.8
        };
        static readonly List<object> _validValues = new List<object>() 
        { 
            0, 
            -42, 
            12.0, 
            "24"
        };
        static readonly List<(object, int)> _validReturnValues = new List<(object input, int output)>() { 
            (0,     0),
            (-42,   -42),
            (12.0,  12),
            ("24",  24)
        };
        [Test]
        [TestCaseSource(nameof(_invalidValues))]
        public void ShouldThrowIfValueIs(object val)
        {
            Assert.That(() => Guard.IsInteger(val!),
                Throws.TypeOf<GuardClauseViolationException>());
        }
        [Test]
        [TestCaseSource(nameof(_validValues))]
        public void ShouldNotThrowExceptionIfValue(object val)
        {
            Assert.That(() => Guard.IsInteger(val),
                Throws.Nothing);
        }
        [Test]
        public void ShouldGiveMessagePointingTowardsNullValueOnViolation()
        {
            Assert.That(() => Guard.IsInteger(""),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : Given value could not be converted to Integer."));
        }
        [Test]
        public void ShouldAddGivenMessageToException()
        {
            Assert.That(() => Guard.IsInteger("", "That's not going to work"),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : That's not going to work"));
        }

        [Test]
        [TestCaseSource(nameof(_validReturnValues))]
        public void ShouldReturnValidValues((object input, int output) val)
        {
            Guard.IsInteger(val.input, out int retVal);
            Assert.That(retVal, Is.EqualTo(val.output));
        }
    }
}
