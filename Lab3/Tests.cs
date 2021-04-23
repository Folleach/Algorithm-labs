using System.Numerics;
using NUnit.Framework;

namespace Lab3
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCaseSource(nameof(AddSource))]
        public void Add(RationNumber first, RationNumber second, RationNumber expected)
        {
            Assert.AreEqual(expected, first + second);
        }
        
        [Test]
        [TestCaseSource(nameof(SubSource))]
        public void Sub(RationNumber first, RationNumber second, RationNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }
        
        [Test]
        [TestCaseSource(nameof(MultipleSource))]
        public void Multiple(RationNumber first, RationNumber second, RationNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }
        
        [Test]
        [TestCaseSource(nameof(DivSource))]
        public void Div(RationNumber first, RationNumber second, RationNumber expected)
        {
            Assert.AreEqual(expected, first - second);
        }

        [Test]
        [TestCaseSource(nameof(GcdSource))]
        public void GreatestCommonDivisor(RationNumber number, int divisor)
        {
            Assert.AreEqual((BigInteger)divisor, number.GreatestCommonDivisor());
        }

        public static object[] AddSource =
        {
            new RationNumber[] { (1, 1), (2, 1), (3, 1) },
            new RationNumber[] { (1, 1), (1, 2), (3, 2) }
        };
        
        public static object[] SubSource =
        {
            new RationNumber[] { (3, 1), (2, 1), (1, 1) },
            new RationNumber[] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] MultipleSource =
        {
            new RationNumber[] { (3, 1), (3, 1), (9, 1) },
            new RationNumber[] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] DivSource =
        {
            new RationNumber[] { (1, 1), (1, 2), (2, 1) },
            new RationNumber[] { (12, 1), (3, 1), (4, 1) }
        };
        
        public static object[] GcdSource =
        {
            new object[] { new RationNumber(9, 12), 3 },
            new object[] { new RationNumber(1, 1), 1 },
            new object[] { new RationNumber(-9, 12), 3 },
            new object[] { new RationNumber(971, 977), 1 },
            new object[] { new RationNumber(41, 82), 41 },
            new object[] { new RationNumber(0, 0), 0 },
        };
    }
}