using System;
using System.Linq;
using System.Text;

namespace Lab2
{
    public static class RandomExtension
    {
        private static readonly char[] EnglishLower = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static readonly char[] EnglishUpper = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'H', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] Numbers = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private static readonly char[] EnglishLowerUpper;
        private static readonly char[] EnglishLowerUpperNumbers;

        static RandomExtension()
        {
            EnglishLowerUpper = EnglishLower.Union(EnglishUpper).ToArray();
            EnglishLowerUpperNumbers = EnglishLowerUpper.Union(Numbers).ToArray();
        }

        public static string NextString(this Random random, int length, char[] alphabet = null)
        {
            if (length < 0)
                return string.Empty;
            var builder = new StringBuilder(length);
            if (alphabet == null)
                alphabet = EnglishLower;
            for (int i = 0; i < length; i++)
                builder.Append(alphabet[random.Next(alphabet.Length)]);
            return builder.ToString();
        }
    }
}