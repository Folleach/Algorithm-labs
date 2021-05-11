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
            Assert.AreEqual(expected, first * second);
        }
        
        [Test]
        [TestCaseSource(nameof(DivSource))]
        public void Div(RatioNumber first, RatioNumber second, RatioNumber expected)
        {
            Assert.AreEqual(expected, first / second);
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

        [TestCase(2, 1, "2")]
        [TestCase(11, 10, "1,1")]
        [TestCase(1, 3, "0,(3)")]
        [TestCase(5, 11, "0,(45)")]
        [TestCase(1, 11, "0,(09)")]
        [TestCase(125, 7, "17,(857142)")]
        [TestCase(19, 60, "0,31(6)")]
        [TestCase(22, 7, "3,(142857)")]
        [TestCase(31111136, 99999999, "0,(31111136)")]
        [TestCase(123456, 999999, "0,(123456)")]
        [TestCase(31, 64, "0,484375")]
        public void ToPeriodicFraction(int first, int second, string expected)
        {
            var input = new RatioNumber(first, second);

            var result = RatioNumber.ToPeriodicFraction(input);

            Assert.AreEqual(expected, result);
        }

        [TestCase("0,5", 1, 2)]
        [TestCase("0", 0, 1)]
        [TestCase("1", 1, 1)]
        [TestCase("28856,4", 144282, 5)]
        public void ToRatio(string number, int a, int b)
        {
            var expected = new RatioNumber(a, b);
            
            var result = RatioNumber.ToRatio(number);
            
            Assert.AreEqual(expected, result);
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
            new RatioNumber[] { (3, 4), (-3, 4), (0, 1) },
        };
        
        public static object[] SubSource =
        {
            new RatioNumber[] { (3, 1), (2, 1), (1, 1) },
            new RatioNumber[] { (1, 1), (1, 2), (1, 2) },
            new RatioNumber[] { (-2, 3), (99, 4), (-305, 12) },
            new RatioNumber[] { (0, 1), (0, 1), (0, 1) },
            new RatioNumber[] { (3, 4), (3, 4), (0, 1) },
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