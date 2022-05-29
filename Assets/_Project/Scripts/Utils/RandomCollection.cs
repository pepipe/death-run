using System.Collections.Generic;
using UnityEngine;

namespace pepipe.Utils {
    public static class RandomCollection {
        //Shuffle Array
        public static void Shuffle<T>(this T[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                var k = UnityEngine.Random.Range(0, n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        //Shuffle List
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                var k = UnityEngine.Random.Range(0, n--);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
