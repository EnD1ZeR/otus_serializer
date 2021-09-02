using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Csv.TypeSerializers.Params
{
    internal sealed class TypeDeserializeParams
    {
        public string Source { get; private set; }
        public Type TypeOfObject { get; private set; }
        public TokenList Tokens { get; private set; }

        public TypeDeserializeParams(string source, Type typeOfObject, TokenList tokens)
        {
            Source = source;
            TypeOfObject = typeOfObject;
            Tokens = tokens;
        }

        public TypeDeserializeParams(TypeDeserializeParams typeDeserializeParams, Type typeOfObject)
        {
            TypeOfObject = typeOfObject;
            Source = typeDeserializeParams.Source;
            Tokens = typeDeserializeParams.Tokens;
        }
    }
}
