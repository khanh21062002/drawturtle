using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{
    public static class Extension
    {
        public static IEnumerable<T> TakeAndRemove<T>(this Queue<T> queue, int count)
        {
            for (int i = 0; i < count && queue.Count > 0; i++)
                yield return queue.Dequeue();
        }
    }
}
