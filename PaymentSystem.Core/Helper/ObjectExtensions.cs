using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Helper
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act)
        {
            foreach (var i in array)
                act(i);
            return array;
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == default;
        }
    }
}
