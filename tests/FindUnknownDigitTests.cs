using System;
using System.Collections.Generic;
using CodeKatas;
using NUnit.Framework;

namespace tests {
    [TestFixture]
    public class FindUnknownDigitTests {

        [Test]
        public void PerformOperationTests () {

            var mult = Runes.PerformOperation ('*', 2, 3);
            Assert.AreEqual (6, mult);

            var add = Runes.PerformOperation ('+', 2, 3);
            Assert.AreEqual (5, add);

            var sub = Runes.PerformOperation ('-', 7, 5);
            Assert.AreEqual (2, sub);

            Assert.Throws<ArgumentOutOfRangeException> (() => Runes.PerformOperation ('q', 2, 3));
        }

        [Test]
        public void CalculateTests_OneOperation_Correct () {

            var operations = new List<char> {
                '-'
            };

            var numbers = new List<int> {
                3,
                2
            };

            var result = Runes.Calculate (operations, numbers);
            Assert.AreEqual (1, result);
        }

        [Test]
        public void CalculateTests_NoOperations_ReturnsNumber () {

            var operations = new List<char> { };

            var numbers = new List<int> {
                3
            };

            var result = Runes.Calculate (operations, numbers);
            Assert.AreEqual (3, result);
        }

        public void CalculateTests_Nothing_ReturnsNoSolutions () {

            var operations = new List<char> { };

            var numbers = new List<int> { };

            var result = Runes.Calculate (operations, numbers);
            Assert.AreEqual (-1, result);
        }

        public void CalculateTests_InvalidState_ThrowsException () {

            var operations = new List<char> { '-' };
            var numbers = new List<int> { };

            Assert.Throws<ArgumentOutOfRangeException> (() => Runes.Calculate (operations, numbers));
        }

        [Test]
        public void AreResolvedMathExpressionsEqualTests () {
            var tests = new [] {
                "1=1",
                "1+2=2+1",
                "2+1=1+2",
                "3-3=1-1",
                "3-2=2+3"
            };

        var result1 = Runes.IsEquationBalanced (tests[0]);
        Assert.AreEqual (true, result1);

        var result2 = Runes.IsEquationBalanced (tests[1]);
        Assert.AreEqual (true, result2);

        var result3 = Runes.IsEquationBalanced (tests[2]);
        Assert.AreEqual (true, result3);

        var result4 = Runes.IsEquationBalanced (tests[3]);
        Assert.AreEqual (false, result4);

        var result5 = Runes.IsEquationBalanced (tests[4]);
        Assert.AreEqual (false, result5);
    }

    [Test]
    public void testSample () {

        Assert.AreEqual (2, Runes.solveExpression ("1+1=?"), "Answer for expression '1+1=?' ");
        Assert.AreEqual (6, Runes.solveExpression ("123*45?=5?088"), "Answer for expression '123*45?=5?088' ");
        Assert.AreEqual (0, Runes.solveExpression ("-5?*-1=5?"), "Answer for expression '-5?*-1=5?' ");
        Assert.AreEqual (-1, Runes.solveExpression ("19--45=5?"), "Answer for expression '19--45=5?' ");
        Assert.AreEqual (5, Runes.solveExpression ("??*??=302?"), "Answer for expression '??*??=302?' ");
        Assert.AreEqual (2, Runes.solveExpression ("?*11=??"), "Answer for expression '?*11=??' ");
        Assert.AreEqual (2, Runes.solveExpression ("??*1=??"), "Answer for expression '??*1=??' ");
        Assert.AreEqual (-1, Runes.solveExpression ("??+??=??"), "Answer for expression '??+??=??' ");
    }
}
}