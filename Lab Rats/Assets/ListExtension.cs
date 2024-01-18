#if UNITY_EDITOR
using System.Collections;
using System.Linq;
using UnityEditorInternal;

public static class ListExtension
{
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
#endif