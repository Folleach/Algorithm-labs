using FluentAssertions;
using Lab2.Sorts;
using NUnit.Framework;

namespace Lab2
{
    [TestFixture]
    public class Tests
    {
        private IterationCounter dummyCounter = new IterationCounter();
        
        [Test]
        public void Radix_Sort_LargestCharShouldBeSort()
        {
            var input = new[] {"baa", "aaa"};
            var expected = new[] {"aaa", "baa"};
            
            Radix.Sort(new FakeArray<string>(input, dummyCounter));
            
            Assert(input, expected);
        }
        
        [Test]
        public void Radix_Sort_AllCharShouldBeSort()
        {
            var input = new []{"aab", "aaa"};
            var expected = new[] {"aaa", "aab"};
            
            Radix.Sort(new FakeArray<string>(input, dummyCounter));
            
            Assert(input, expected);
        }
        
        [Test]
        public void Radix_Sort_WithDifferentLength()
        {
            var input = new []{"hello", "zero", "alpha"};
            var expected = new[] {"alpha", "hello", "zero"};
            
            Radix.Sort(new FakeArray<string>(input, dummyCounter));
            
            Assert(input, expected);
        }
        
        [Test]
        public void Radix_Sort_Permutations()
        {
            var input = new []{"abb", "aba", "aab", "aaa"};
            var expected = new[] {"aaa", "aab", "aba", "abb"};
            
            Radix.Sort(new FakeArray<string>(input, dummyCounter));
            
            Assert(input, expected);
        }

        private void Assert(string[] input, string[] expected)
        {
            input.Length.Should().Be(expected.Length);
            for (var i = 0; i < input.Length; i++)
                input[i].Should().Be(expected[i]);
        }
    }
}