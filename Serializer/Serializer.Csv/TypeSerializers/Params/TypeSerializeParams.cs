using Serializer.Csv.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Csv.TypeSerializers.Params
{
    internal sealed class TypeSerializeParams
    {
        public int CurrentNesting { get; private set; }
        public object Object { get; private set; }
        public SerializationEnvironment Environment { get; private set; }
        public CsvSerializerOptions CsvSerializerOptions { get; private set; }


        public TypeSerializeParams(object @object, SerializationEnvironment environment,
            CsvSerializerOptions csvSerializerOptions)
        {
            Object = @object;
            Environment = environment;
            CsvSerializerOptions = csvSerializerOptions;
            CurrentNesting = 0;
        }

        public TypeSerializeParams(TypeSerializeParams typeSerializeParams, object @object)
        {
            Object = @object;
            Environment = new(typeSerializeParams.Environment);
            CsvSerializerOptions = typeSerializeParams.CsvSerializerOptions;
            CurrentNesting = typeSerializeParams.CurrentNesting + 1;
        }

        public bool NestingLevelIsCorrect()
        {
            return CurrentNesting < CsvSerializerOptions.Nesting;
        }

        public bool CanFallInside()
        {
            return CurrentNesting + 1 < CsvSerializerOptions.Nesting || CurrentNesting == CsvSerializerOptions.Nesting;
        }
    }
}
