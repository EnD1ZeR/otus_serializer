using Serializer.Abstractions;
using Serializer.Csv.TypeSerializers.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Csv.Abstractions
{
    internal interface ICsvTypeSerializer : ITypeSerializer<string, TypeSerializeParams, TypeDeserializeParams>
    {
    }
}
