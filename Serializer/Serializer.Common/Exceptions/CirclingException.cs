using System;
using System.Collections.Generic;
using System.Text;

namespace Serializer.Common.Exceptions
{
    public class CirclingException : Exception
    {
        private const string _exceptionMessage = "One or more objects referencing each other.";
        public CirclingException()
            : base(_exceptionMessage) { }

        public CirclingException(string message)
            : base(message) { }

        public CirclingException(string message, Exception inner)
            : base(message, inner) { }

        public CirclingException(object obj)
            : base(_exceptionMessage + $" Name of field: { obj.GetType().Name }") { }
    }
}
