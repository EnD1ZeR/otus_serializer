using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serializer.Csv.Token;

namespace Serializer.Csv
{
    /// <summary>
    /// Class for working with tokens, parsed from source string
    /// </summary>
    internal sealed class TokenList
    {
        private List<CsvToken> _tokens;
        private string _source;
        private string _delimeter;
        private int _currentTokenIndex;

        private TokenList(string source, string delimeter)
        {
            _tokens = new();
            _source = source;
            _delimeter = delimeter;
            _currentTokenIndex = 0;
        }

        public static TokenList Parse(string source, string delimeter)
        {
            TokenList tokenList = new(source, delimeter);
            tokenList.Parse();

            return tokenList;
        }

        public CsvToken GetToken(int position)
        {
            if (_tokens.Count == position)
            {
                return null;
            }
            return _tokens.ElementAt(position);
        }

        public CsvToken GetNextToken()
        {
            var token = GetToken(_currentTokenIndex);
            _currentTokenIndex++;
            return token;
        }

        private void Parse()
        {
            var values = _source.Split(_delimeter);
            foreach (var value in values)
            {
                _tokens.Add(ParseToken(value));
            }
        }

        private CsvToken ParseToken(string item)
        {
            return new CsvToken(null, item);
        }
    }
}
