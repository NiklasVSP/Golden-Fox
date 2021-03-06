﻿using System;
using System.Collections.Generic;

using GoldenFox;
using GoldenFox.Internal;
using GoldenFox.Internal.Constraints;
using GoldenFox.Internal.Operators;
using GoldenFox.Internal.Operators.Intervals;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryDayTests
    {
        [Test]
        public void EveryDayAt0630FromSameTimeInclusive()
        {
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            var expected = new DateTime(2015, 10, 5, 6, 30, 0);

            Assert.AreEqual(expected, new First(new List<IOperator> { new Day(new Timestamp(6, 30)) }).Evaluate(@from, true));
        }

        [Test]
        public void EveryDayAt0630FromJustBefore()
        {
            var from = new DateTime(2015, 10, 05, 6, 20, 0);
            var expected = new DateTime(2015, 10, 5, 6, 30, 0);

            Assert.AreEqual(expected, new First(new List<IOperator> { new Day(new Timestamp(6, 30)) }).Evaluate(@from));
        }

        [Test]
        public void EveryDayAt0630FromJustAfter()
        {
            var from = new DateTime(2015, 10, 5, 6, 40, 0);
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            Assert.AreEqual(expected, new First(new List<IOperator> { new Day(new Timestamp(6, 30)) }).Evaluate(@from));
        }

        [Test]
        public void EveryDayAt0630FromBeforeFrom()
        {
            var from = new DateTime(2015, 10, 5, 6, 40, 0);
            var expected = new DateTime(2016, 1, 1, 6, 30, 0);
            var sut = new Day(new Timestamp(6, 30));
            sut.AddConstraint(new From(new DateTime(2016, 1, 1)));
            Assert.AreEqual(expected, sut.Evaluate(@from));
        }

        [Test]
        public void EveryDayAtTwoTimesOneLaterSameDayOneEarlierNextDay()
        {
            var expected = new DateTime(2015, 10, 5, 8, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 30, 0);

            Assert.AreEqual(
                expected,
                new First(new List<IOperator> { new Day(new Timestamp(5, 30)), new Day(new Timestamp(8, 30)) }).Evaluate(@from));
        }

        [Test]
        public void EveryDayAtTwoTimesBothLaterSameDay()
        {
            var expected = new DateTime(2015, 10, 5, 7, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(expected, new First(new List<IOperator> { new Day(new Timestamp(7, 30)), new Day(new Timestamp(8, 30)) }).Evaluate(@from));
        }

        [Test]
        public void EveryDayAt0630FromSameTimeDoNotInclusive()
        {
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(expected, new Day(new Timestamp(6, 30)).Evaluate(from));
        }
    }
}
