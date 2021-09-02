using Serializer.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serializer.Csv.Common
{
    /// <summary>
    /// Environment for reflected object
    /// </summary>
    internal sealed class SerializationEnvironment
    {
        private List<Type> _objectTypes = new();

        public SerializationEnvironment() { }

        public SerializationEnvironment(SerializationEnvironment environment)
        {
            _objectTypes = new(environment._objectTypes);
        }

        /// <summary>
        /// Add an object type to the list for subsequent checking for circular dependency
        /// </summary>
        /// <param name="obj"></param>
        public void AddObject(object obj)
        {
            if (obj is null)
            {
                return;
            }

            var objectType = obj.GetType();

            if (IsCirclingExist(objectType))
            {
                throw new CirclingException(obj);
            }

            _objectTypes.Add(objectType);
        }

        private bool IsCirclingExist(Type objectType)
        {
            return _objectTypes.Any(t => t == objectType);
        }
    }
}
