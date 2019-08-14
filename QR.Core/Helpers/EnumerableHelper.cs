using System;
using System.Collections.Generic;

namespace QR.Core
{
    internal static class EnumerableHelper
    {
        public static IEnumerable<IEnumerable<T>> Bucket<T>(this IEnumerable<T> source, int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            IEnumerator<T> enumerator = source.GetEnumerator();
            bool enumerating = false;

            do
            {
                List<T> currentBucket = new List<T>(size);
                for (int i = 0; i < size && (enumerating = enumerator.MoveNext()); ++i)
                    currentBucket.Add(enumerator.Current);
                if (currentBucket.Count != 0)
                    yield return currentBucket;
            }
            while (enumerating);
        }
    }
}
