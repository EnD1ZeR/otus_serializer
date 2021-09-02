using Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serializer.Common
{
    public abstract class SerializerOptions : ISerializerOptions
    {
        public int Nesting { get; private set; }

        protected SerializerOptions(int nesting)
        {
            Nesting = nesting;
        }
    }
}
