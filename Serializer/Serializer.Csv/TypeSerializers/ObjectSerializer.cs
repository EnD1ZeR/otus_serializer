using Serializer.Abstractions;
using Serializer.Common.Exceptions;
using Serializer.Csv.Abstractions;
using Serializer.Csv.TypeSerializers.Params;
using Serializer.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Serializer.Csv.TypeSerializers
{
    internal sealed class ObjectSerializer : ICsvTypeSerializer
    {
        private TypeSerializer _typeSerializer;
        public ObjectSerializer(TypeSerializer typeSerializer)
        {
            _typeSerializer = typeSerializer;
        }

        public object Deserialize(TypeDeserializeParams deserializeParams)
        {
            var currentType = deserializeParams.TypeOfObject;
            var instance = Activator.CreateInstance(currentType);

            foreach (MemberInfo member in currentType.GetFieldsAndProperties())
            {
                var memberType = member.GetMemberType();
                ICsvTypeSerializer serializer = _typeSerializer.GetSerializer(memberType);
                var deserializedValue = serializer.Deserialize(new TypeDeserializeParams(deserializeParams, memberType));
                member.SetValue(instance, deserializedValue);
            }

            return instance;
        }

        public string Serialize(TypeSerializeParams serializeParams)
        {
            if (IsArray(serializeParams.Object))
            {
                throw new TypeNotSupportedException(serializeParams.Object);
            }

            if (serializeParams.CsvSerializerOptions.CheckCircularDependency)
            {
                serializeParams.Environment.AddObject(serializeParams.Object);
            }

            List<string> propsAndFields = new();
            Type objType = serializeParams.Object.GetType();

            foreach (MemberInfo member in objType.GetFieldsAndProperties())
            {
                var memberType = member.GetMemberType();
                ICsvTypeSerializer serializer = _typeSerializer.GetSerializer(memberType);

                var value = member.GetValue(serializeParams.Object);
                
                propsAndFields.Add(serializer.Serialize(new TypeSerializeParams(serializeParams, value)));
            }

            return string.Join(serializeParams.CsvSerializerOptions.Delimeter, propsAndFields);
        }

        private bool IsArray(object obj)
        {
            return obj.GetType().IsArray;
        }
    }
}
