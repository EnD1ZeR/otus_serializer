using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Common.Exceptions
{
    public class NestingException : Exception
    {
        private const string _exceptionMessage = "Nesting has gone beyond the limits.";
        public NestingException()
            : base(_exceptionMessage) { }

        public NestingException(string message)
            : base(message) { }

        public NestingException(string message, Exception inner)
            : base(message, inner) { }
    }
}
