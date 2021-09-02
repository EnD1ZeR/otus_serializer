using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Csv.Token
{
    internal class CsvToken
    {
        /// <summary>
        /// Name of the field
        /// </summary>
        /// <remarks>
        /// Not used
        /// </remarks>
        public string Name { get; private set; }
        public string Value { get; private set; }

        public CsvToken(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
