using System;
using System.Collections.Generic;
using System.Text;

using Serializer.Abstractions;
using Serializer.Common;

namespace Serializer.Csv.Common
{
    /// <summary>
    /// Options for CSV serializer
    /// </summary>
    public sealed class CsvSerializerOptions : SerializerOptions, ISerializerOptions
    {
        /// <summary>
        /// Delimeter
        /// </summary>
        /// <value>"," by default</value>
        public string Delimeter { get; set; }

        /// <summary>
        /// Enable checking circular dependency
        /// </summary>
        /// <value>
        /// <c>true</c> by default
        /// </value>
        public bool CheckCircularDependency { get; set; }

        public CsvSerializerOptions(int nesting = 0)
            : base(nesting)
        {
            CheckCircularDependency = true;
            Delimeter = ",";
        }
    }
}
