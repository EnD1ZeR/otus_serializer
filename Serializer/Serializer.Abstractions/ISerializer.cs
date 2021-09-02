using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Abstractions
{
    public interface ISerializer
    {
        public string SerializeFromObject(object obj);
        public TObject DeserializeToObject<TObject>(string source);
    }
}
