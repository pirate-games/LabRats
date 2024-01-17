using System.Collections;
using System.Linq;
using UnityEditorInternal;

namespace Global.Tools
{
    public static class ListExtension
    {
        /// <summary>
        ///  Returns a reorder-able list that can be used in the inspector
        /// </summary>
        /// <param name="list"> the list that should be shown in the GUI </param>
        /// <returns> a GUi ready list that can be shown in the inspector </returns>
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
        ///  Returns a reorder-able list that can be used in the inspector 
        ///  This time in its generic form so you can specify the type of the list
        /// </summary>
        /// <param name="list"> the list to be shown in the GUI </param>
        /// <typeparam name="T"> the specified type of the list </typeparam>
        /// <returns> a GUI ready list of the specified type that can be shown in the inspector </returns>
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