using System.Numerics;
using NUnit.Framework;

namespace Lab3
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCaseSource(nameof(AddSource))]
        public void Add(RatioNumber first, RatioNumber second, RatioNumber expected)
        {
            Assert.AreEqual(expected, first + second);
        }
        
        [Test]
        [TestCaseSource(nameof(SubSource))]
        public void Sub(RatioNumber first, RatioNumber second, RatioNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }
        
        [Test]
        [TestCaseSource(nameof(MultipleSource))]
        public void Multiple(RatioNumber first, RatioNumber second, RatioNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }
        
        [Test]
        [TestCaseSource(nameof(DivSource))]
        public void Div(RatioNumber first, RatioNumber second, RatioNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }

        [Test]
        [TestCaseSource(nameof(GcdSource))]
        public void GreatestCommonDivisor(RatioNumber number, int divisor)
        {
            Assert.AreEqual((BigInteger)divisor, number.GreatestCommonDivisor());
        }
        
        [Test]
        [TestCaseSource(nameof(SimplifySource))]
        public void Simplify(RatioNumber number, RatioNumber expected)
        {
            Assert.AreEqual(expected, number.Simplify());
        }

        public static object[] AddSource =
        {
            new RatioNumber[] { (1, 1), (2, 1), (3, 1) },
            new RatioNumber[] { (1, 1), (1, 2), (3, 2) },
            new RatioNumber[] { (0, 1), (0, 1), (0, 1) },
            new RatioNumber[] { (0, 2), (0, 2), (0, 1) },
            new RatioNumber[] { (-2, 1), (3, 1), (1, 1) },
            new RatioNumber[] { (-10, 1), (-10, 1), (-20, 1) },
            new RatioNumber[] { (1, 2), (1, 3), (5, 6) },
            new RatioNumber[] { (100, 2), (50, 1), (100, 1) },
        };
        
        public static object[] SubSource =
        {
            new RatioNumber[] { (3, 1), (2, 1), (1, 1) },
            new RatioNumber[] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] MultipleSource =
        {
            new RatioNumber[] { (3, 1), (3, 1), (9, 1) },
            new RatioNumber[] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] DivSource =
        {
            new RatioNumber[] { (1, 1), (1, 2), (2, 1) },
            new RatioNumber[] { (12, 1), (3, 1), (4, 1) }
        };
        
        public static object[] GcdSource =
        {
            new object[] { new RatioNumber(9, 12), 3 },
            new object[] { new RatioNumber(1, 1), 1 },
            new object[] { new RatioNumber(-9, 12), 3 },
            new object[] { new RatioNumber(971, 977), 1 },
            new object[] { new RatioNumber(41, 82), 41 },
            new object[] { new RatioNumber(0, 0), 0 },
        };
        
        public static object[] SimplifySource =
        {
            new RatioNumber[] { (1, 1), (1, 1) },
            new RatioNumber[] { (9, 12), (3, 4) },
            new RatioNumber[] { (12, 9), (4, 3) },
            new RatioNumber[] { (-24, 12), (-2, 1) },
        };
    }
}