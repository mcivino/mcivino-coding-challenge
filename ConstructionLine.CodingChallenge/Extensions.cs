using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionLine.CodingChallenge
{
    public static class Extensions
    {
        public static IEnumerable<List<T>> Split<T>(this List<T> list, int size = 10)
        {
            for (var i = 0; i < list.Count; i += size)
            {
                yield return list.GetRange(i, Math.Min(size, list.Count - i));
            }
        }
    }
}
