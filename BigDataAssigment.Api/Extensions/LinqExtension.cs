using System;
using System.Collections.Generic;

namespace BigDataAssigment.Api.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
            return source;
        }
    }
}
