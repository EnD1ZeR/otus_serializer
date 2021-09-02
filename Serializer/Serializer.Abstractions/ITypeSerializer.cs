using System;
using System.Collections.Generic;
using System.Text;

namespace Serializer.Abstractions
{
    public interface ITypeSerializer<TOut, TSerializeParams, TDeserializeParams>
        where TOut : class
        where TSerializeParams : class
        where TDeserializeParams : class
    {
        public TOut Serialize(TSerializeParams serializeParams);
        public object Deserialize(TDeserializeParams deserializeParams);
    }
}
