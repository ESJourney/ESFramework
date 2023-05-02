using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventSourcing
{
    public abstract class StringEnum<T>
    {
        private static readonly IEnumerable<string> list = Ensured.AllUniqueConstStrings<T>();

        public static IEnumerable<string> EnumList => list;

        public static bool IsValid(string value)
        {
            return list.Contains(value);
        }
    }
}
