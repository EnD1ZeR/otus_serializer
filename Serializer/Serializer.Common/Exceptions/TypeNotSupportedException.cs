using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Common.Exceptions
{
    public class TypeNotSupportedException : Exception
    {
        private const string _exceptionMessage = "This type won't be serialize.";
        public TypeNotSupportedException()
            : base(_exceptionMessage) { }

        public TypeNotSupportedException(string message)
            : base(message) { }

        public TypeNotSupportedException(string message, Exception inner)
            : base(message, inner) { }

        public TypeNotSupportedException(object obj)
            : base(_exceptionMessage + $" Name of field: { obj.GetType().FullName }") { }
    }
}
