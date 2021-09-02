using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serializer.Abstractions;
using Serializer.Common.Exceptions;
using Serializer.Csv.Common;
using Serializer.Csv.TypeSerializers;
using Serializer.Csv.TypeSerializers.Params;

namespace Serializer.Csv
{
    public sealed class CsvSerializer : ISerializer
    {
        private CsvSerializerOptions _options;

        public CsvSerializer(CsvSerializerOptions options = null)
        {
            _options = options ?? new();
        }

        public T DeserializeToObject<T>(string source)
        {
            TypeSerializer typeSerializer = new();
            Type objectType = typeof(T);
            TokenList tokens = TokenList.Parse(source, _options.Delimeter);

            return (T)typeSerializer.GetSerializer(objectType)
                .Deserialize(new TypeDeserializeParams(source, objectType, tokens));
        }

        public string SerializeFromObject(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type typeOfObject = obj.GetType();
            TypeSerializer typeSerializer = new();
            TypeSerializeParams typeSerializeParams = new(obj, new SerializationEnvironment(), _options);
            
            return typeSerializer.GetSerializer(typeOfObject)
                .Serialize(typeSerializeParams);
        }
    }
}
