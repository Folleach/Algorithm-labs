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
        public void Simple(string input, double expected)
        {
            var expression = new PolishNotation(input);
            
            var actual = expression.Calc();
            
            Assert.AreEqual(expected, actual);
        }
    }
}