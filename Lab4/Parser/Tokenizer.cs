using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public class Tokenizer : IEnumerable<Token>
    {
        private readonly Dictionary<TokenType, Token> currentByType = new Dictionary<TokenType, Token>();
        private readonly List<Token> currentLineTokens = new List<Token>();
        private readonly LinkedList<Token> tokens = new LinkedList<Token>();
        private int line;
        private readonly string text;

        public Tokenizer(string text)
        {
            this.text = text;
            ParseToToken();
        }

        public Token First => tokens.First?.Value;

        public IEnumerator<Token> GetEnumerator()
        {
            return tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ParseToToken()
        {
            tokens.Clear();
            if (string.IsNullOrEmpty(text))
                return;
            var currentType = GetTokenType(text.First());
            var builder = new StringBuilder();
            var tokenIndex = 0;
            for (var i = 0; i < text.Length; ++i)
            {
                var type = GetTokenType(text[i]);
                if (type == TokenType.Space)
                    continue;
                if (currentType != type)
                {
                    AddToken(currentType, builder.ToString(), tokenIndex);
                    builder.Clear();
                    tokenIndex = i;
                    currentType = type;
                }

                builder.Append(text[i]);
            }

            AddToken(currentType, builder.ToString(), tokenIndex);
        }

        private Token ReadTagToken(int index)
        {
            var builder = new StringBuilder();
            var i = index;
            string currentValid;
            do
            {
                builder.Append(text[i]);
                currentValid = builder.ToString();
                ++i;
            } while (i < text.Length);

            return AddToken(TokenType.Tag, currentValid, index);
        }

        private void SetNextLine(Token token)
        {
            if (currentLineTokens.Count == 0)
            {
                currentLineTokens.Add(token);
                return;
            }

            var containsLine = currentLineTokens[0].Line;
            if (token.Line == containsLine)
            {
                currentLineTokens.Add(token);
                return;
            }

            foreach (var previousToken in currentLineTokens)
                previousToken.SetNextLine(token);
            currentLineTokens.Clear();
            currentLineTokens.Add(token);
        }

        private void SetNextSomeType(Token token)
        {
            if (!currentByType.ContainsKey(token.Type))
            {
                currentByType.Add(token.Type, token);
                return;
            }

            var previous = currentByType[token.Type];
            previous.SetSomeNext(token);
            currentByType[token.Type] = token;
        }

        private TokenType GetTokenType(char value)
        {
            if (value == '\n')
                return TokenType.BreakLine;
            if (char.IsDigit(value))
                return TokenType.Number;
            if (char.IsWhiteSpace(value))
                return TokenType.Space;
            return char.IsLetterOrDigit(value)
                ? TokenType.Word
                : TokenType.SymbolSet;
        }
        
        private Token AddToken(TokenType type, string value, int index)
        {
            var token = new Token(type, value, index, tokens.Last?.Value, line);
            tokens.Last?.Value.SetNext(token);
            tokens.AddLast(token);
            SetNextSomeType(token);
            SetNextLine(token);
            if (token.Type == TokenType.BreakLine)
                ++line;
            return token;
        }

        private TokenType GetTokenOnIndex(int index)
        {
            if (index >= text.Length)
                return TokenType.Undefined;
            var value = text[index];
            if (value == '\n')
                return TokenType.BreakLine;
            if (char.IsWhiteSpace(value))
                return TokenType.Space;
            if (char.IsNumber(value))
                return TokenType.Number;
            if (char.IsLetter(value))
                return TokenType.Word;
            return TokenType.SymbolSet;
        }
    }
}