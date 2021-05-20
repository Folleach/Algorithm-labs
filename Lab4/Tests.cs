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
            var expression = new PolishNotation(input);
            var actual = expression.Calc();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase("(2 + 2) * 2", 8)]
        [TestCase("2 * (3 - 4)", -2)]
        [TestCase("3 + (4 * 10)", 43)]
        public void WithBrackets(string input, double expected)
        {
            var expression = new PolishNotation(input);
            var actual = expression.Calc();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase("x + 1", 3, 4)]
        [TestCase("x * x", 4, 16)]
        public void WithVariables(string input, double xValue, double expected)
        {
            var expression = new PolishNotation(input);
            var actual = expression.Calc(("x", xValue));
            Assert.AreEqual(expected, actual);
        }
    }
}