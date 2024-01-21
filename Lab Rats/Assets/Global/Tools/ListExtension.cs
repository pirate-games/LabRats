#if UNITY_EDITOR
using System.Collections;
using System.Linq;
using UnityEditorInternal;

namespace Global.Tools
{
    public static class ListExtension
    {
        /// <summary>
        ///  Returns a reorderable list for the given list
        /// </summary>
        /// <param name="list"> the list to reorder </param>
        /// <returns></returns>
        public static ReorderableList GetImmutableGUIList(this IList list)
        {
            return new ReorderableList(
                list,
                list.GetType().GetGenericArguments().FirstOrDefault(),
                false,
                false,
                false,
                false
            );
        }
        
        /// <summary>
        ///  Returns a reorderable list for the given list
        /// </summary>
        /// <param name="list"> the list to reorder </param>
        /// <typeparam name="T"> the type of list  </typeparam>
        /// <returns></returns>
        public static ReorderableList GetImmutableGUIList<T>(this IList list)
        {
            return new ReorderableList(
                list,
                typeof(T),
                false,
                false,
                false,
                false
            );
        }
    }
}
#endif