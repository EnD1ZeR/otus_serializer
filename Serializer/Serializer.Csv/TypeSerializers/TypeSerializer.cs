using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serializer.Csv.Abstractions;
using Serializer.Reflection.Extensions;

namespace Serializer.Csv.TypeSerializers
{
    internal sealed class TypeSerializer
    {
        private ObjectSerializer _objectSerializer;
        private SimpleSerializer _primitiveSerializer = new();

        public TypeSerializer()
        {
            _objectSerializer = new(this);
        }

        public ICsvTypeSerializer GetSerializer(Type objType)
        {
            if (objType.IsSimpleType())
            {
                return _primitiveSerializer;
            }
            else
            {
                return _objectSerializer;
            }
        }
    }
}
