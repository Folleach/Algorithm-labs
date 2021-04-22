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

        public static object[] AddSource =
        {
            new RationNumber[] { (1, 1), (2, 1), (3, 1) },
            new RationNumber[] { (1, 1), (1, 2), (3, 2) }
        };
        
        public static object[] SubSource =
        {
            new [] { (3, 1), (2, 1), (1, 1) },
            new [] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] MultipleSource =
        {
            new [] { (3, 1), (3, 1), (9, 1) },
            new [] { (1, 1), (1, 2), (1, 2) }
        };
        
        public static object[] DivSource =
        {
            new [] { (1, 1), (1, 2), (2, 1) },
            new [] { (12, 1), (3, 1), (4, 1) }
        };
    }
}