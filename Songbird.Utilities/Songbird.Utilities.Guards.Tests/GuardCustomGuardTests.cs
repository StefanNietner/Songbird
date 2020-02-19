using NUnit.Framework;
using Songbird.Utilities.Guards;
using Songbird.Utilities.Guards.Exceptions;
using System;
using System.Collections.Generic;

namespace Songbird.Guards.Tests
{
    public class GuardCustomGuardTests
    {
        private static readonly List<(string, Func<string, bool>, Func<bool>, Func<bool>, string)> _nullCases =
            new List<(string, Func<string, bool>, Func<bool>, Func<bool>, string)>()
        {
            ("", null,          ()=>true,   ()=>false,  "The given check function was null."),
            ("", s=>s!=null,    null,       ()=>false,  "The given onSuccess function was null."),
            ("", s=>s!=null,    ()=>true,   null,       "The given onFailure function was null."),
        };

        [Test]
        [TestCaseSource(nameof(_nullCases))]
        public void ShouldThrowWithMessage(
            (string value, Func<string, bool> check, Func<bool> onSuccess, Func<bool> onFailure, string message) input)
        {
            Assert.That(() => Guard.CustomGuard(input.value, input.check, input.onSuccess, input.onFailure),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo($"Guard clause violated. : {input.message}"));
        }

        [Test]
        [TestCase(42)]
        [TestCase(12)]
        public void ShouldExecuteCheckFunctionWithGivenValue(int value)
        {
            int valueCheck = 0;
            bool check(int i) { valueCheck = i; return true; }
            static bool onSuccess() { return true; }
            static bool onFailure() { return false; }
            Guard.CustomGuard(value, check, onSuccess, onFailure);
            Assert.That(valueCheck, Is.EqualTo(value));
        }

        [Test]
        public void ShouldExecuteAndReturnOnSuccessFunctionIfCheckReturnsTrue()
        {
            bool wasExecuted = false;
            static bool check(int i) { return true; }
            bool onSuccess() { wasExecuted = true; return true; }
            static bool onFailure() { return false; }
            var retVal = Guard.CustomGuard(0, check, onSuccess, onFailure);
            Assert.That(retVal, Is.True);
            Assert.That(wasExecuted, Is.True);
        }
        [Test]
        public void ShouldExecuteAndReturnOnFailureFunctionIfCheckReturnsFalse()
        {
            bool wasExecuted = false;
            static bool check(int i) { return false; }
            static bool onSuccess() { return true; }
            bool onFailure() { wasExecuted = true; return false; }
            var retVal = Guard.CustomGuard(0, check, onSuccess, onFailure);
            Assert.That(retVal, Is.False);
            Assert.That(wasExecuted, Is.True);
        }

        private static readonly List<(int, Func<int, bool>, string)> _checkOnlyValues = new List<(int, Func<int, bool>, string)>()
        {
            (42, (i) => i < 0, "42 is greater than zero"),
            (0, (i) => i != 0, "zero is never not equal to zero"),
        };
        [Test]
        [TestCaseSource(nameof(_checkOnlyValues))]
        public void ShouldThrowForFailedCheck((int value, Func<int, bool> check, string message) input)
        {
            Assert.That(() => Guard.CustomGuard(input.value, input.check),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : Custom check failed without a message."));
        }
        [Test]
        public void ShouldThrowIfCheckIsNull()
        {
            Assert.That(() => Guard.CustomGuard(42, null),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : The given check function was null."));
        }
        [Test]
        [TestCaseSource(nameof(_checkOnlyValues))]
        public void ShouldThrowWithCustomMessageForFailedCheck((int value, Func<int, bool> check, string message) input)
        {
            Assert.That(() => Guard.CustomGuard(input.value, input.check, input.message),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo($"Guard clause violated. : {input.message}"));
        }

        private static readonly List<(int, Func<int, bool>, Func<bool>, string)> _noFailureInvalidValues =
            new List<(int, Func<int, bool>, Func<bool>, string)>()
            {
                (42, (i) => i < 0,  ()=>true, "42 is greater than zero"),
                (0,  (i) => i != 0, ()=>true, "zero is never not equal to zero"),
            };
        [Test]
        [TestCaseSource(nameof(_noFailureInvalidValues))]
        public void ShouldThrowForFailedCheck((int value, Func<int, bool> check, Func<bool> onSuccess, string message) input)
        {
            Assert.That(() => Guard.CustomGuard(input.value, input.check, input.onSuccess),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo("Guard clause violated. : Custom check failed without a message."));
        }
        [Test]
        [TestCaseSource(nameof(_noFailureInvalidValues))]
        public void ShouldThrowWithCustomMessageForFailedCheck((int value, Func<int, bool> check, Func<bool> onSuccess, string message) input)
        {
            Assert.That(() => Guard.CustomGuard(input.value, input.check, input.onSuccess, input.message),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo($"Guard clause violated. : {input.message}"));
        }

        private static readonly List<(int, Func<int, bool>, Func<string>, string)> _noFailureValidValues =
            new List<(int, Func<int, bool>, Func<string>, string)>()
            {
                (42, (i) => i > 0,  ()=>"42 is a valid number", "42 is a valid number"),
                (0,  (i) => i == 0, ()=>"0 is a valid number",  "0 is a valid number"),
            };
        [Test]
        [TestCaseSource(nameof(_noFailureValidValues))]
        public void ShouldExecuteAndReturnOnSuccessFunctionIfCheckReturnsTrue((int value, Func<int, bool> check, Func<string> onSuccess, string message) input)
        {
            var retVal = Guard.CustomGuard(input.value, input.check, input.onSuccess);
            Assert.That(retVal, Is.EqualTo(input.message));
        }

        private static readonly List<(int, Func<int, bool>, Func<bool>, string)> _noFailureNullValues =
            new List<(int, Func<int, bool>, Func<bool>, string)>()
            {
                (42, null,          ()=>true, "The given check function was null."),
                (0,  (i) => i != 0, null,     "The given onSuccess function was null."),
            };
        [Test]
        [TestCaseSource(nameof(_noFailureNullValues))]
        public void ShouldThrowIfDelegateIsNull((int value, Func<int, bool> check, Func<bool> onSuccess, string message) input)
        {
            Assert.That(()=>Guard.CustomGuard(input.value, input.check, input.onSuccess),
                Throws.TypeOf<GuardClauseViolationException>()
                .With.Message.EqualTo($"Guard clause violated. : {input.message}"));
        }
    }
}
