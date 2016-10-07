namespace MeetEric.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class CollectionExtensions
    {
        public static void AddRangeSafe<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }

        public static void AddIfNotNull<T>(this ICollection<T> list, T item)
            where T : class
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
    }
}
