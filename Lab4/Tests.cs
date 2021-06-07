using System;
using NUnit.Framework;

namespace Lab4
{
    [TestFixture]
    public class Tests
    {
        [TestCase("1 + 1", 2)]
        [TestCase("1 + 1 + 7", 9)]
        [TestCase("2 + 2 * 2", 6)]
        [TestCase("2 * 2 + 2", 6)]
        [TestCase("9 - 11", -2)]
        [TestCase("15 / 5", 3)]
        [TestCase("19", 19)]
        public void Simple(string input, double expected)
        {
            var expression = new PolishAlgorithm(input);
            var actual = expression.Calculate();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase("(2 + 2) * 2", 8)]
        [TestCase("2 * (3 - 4)", -2)]
        [TestCase("3 + (4 * 10)", 43)]
        [TestCase("((((4)))) + 1", 5)]
        public void WithBrackets(string input, double expected)
        {
            var expression = new PolishAlgorithm(input);
            var actual = expression.Calculate();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase("x + 1", 3, 4)]
        [TestCase("x * x", 4, 16)]
        [TestCase("(4 + x) * x", 10, 140)]
        public void WithVariables(string input, double xValue, double expected)
        {
            var expression = new PolishAlgorithm(input);
            var actual = expression.Calculate(("x", xValue));
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase("cos(x)", Math.PI, -1)]
        [TestCase("sin(x + 1)", Math.PI / 2 - 1, 1)]
        [TestCase("abs(10 - 100) + x", 1, 91)]
        public void WithFunctions(string input, double xValue, double expected)
        {
            var expression = new PolishAlgorithm(input);
            var actual = expression.Calculate(("x", xValue));
            Assert.AreEqual(expected, actual);
        }
    }
}