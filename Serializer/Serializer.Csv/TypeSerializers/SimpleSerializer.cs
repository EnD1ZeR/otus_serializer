using Serializer.Abstractions;
using Serializer.Csv.Abstractions;
using Serializer.Csv.TypeSerializers.Params;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serializer.Csv.TypeSerializers
{
    internal sealed class SimpleSerializer : ICsvTypeSerializer
    {
        public object Deserialize(TypeDeserializeParams deserializeParams)
        {
            var token = deserializeParams.Tokens.GetNextToken();

            if (deserializeParams.TypeOfObject == typeof(Guid) || deserializeParams.TypeOfObject == typeof(Guid?))
            {
                return Guid.Parse(token.Value);
            }

            return Convert.ChangeType(token.Value, deserializeParams.TypeOfObject);
        }

        public string Serialize(TypeSerializeParams serializeParams)
        {
            return serializeParams.Object.ToString();
        }
    }
}
