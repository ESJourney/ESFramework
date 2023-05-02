using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public static class Ensured
    {
        public static string NotEmpty(string? argument, string argumentName)
        {
            Ensure.NotEmpty(argument, argumentName);
            return argument!;
        }
        public static T NotNull<T>(T? argument, string argumentName) where T : class
        {
            Ensure.NotNull(argument, argumentName);
            return argument!;
        }
        public static IEnumerable<string> AllUniqueConstStrings<T>(params Type[] moreTypes)
        {
            var pendingQueue = new Queue<Type>();
            pendingQueue.Enqueue(typeof(T));

            for (int i = 0; i < moreTypes.Length; i++)
                pendingQueue.Enqueue(moreTypes[i]);

            var stringList = new List<string>();

            while (pendingQueue.TryDequeue(out var currentType))
            {
                var types = currentType.GetNestedTypes();
                foreach (var t in types)
                    pendingQueue.Enqueue(t);


                var strings = currentType.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(string))
                    .Select(f => (string)f.GetValue(null));

                stringList.AddRange(strings);
            }

            if (stringList.Count() != stringList.Distinct().Count())
                throw new InvalidOperationException($"The type {typeof(T).FullName} does not contain unique constants strings.");

            return stringList;
        }
    }
}
