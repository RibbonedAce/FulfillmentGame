using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    /// <summary>
    /// Rotate the transform to have the right direction facing the target while keeping the x and y axes relative to a 2D space
    /// </summary>
    /// <param name="transform">The transform to rotate</param>
    /// <param name="target">The target transform to look at</param>
    /// <param name="stayFacedUp">If true, will keep rotation between -90 and 90 to keep the transform's up direction facing the global up direction</param>
    public static void LookAt2D(this Transform transform, Transform target, bool stayFacedUp) {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        float orientation = 0;

        if (stayFacedUp && Mathf.Abs(angle) > 90) {
            orientation = 180;
            angle = 180 - angle;
        }

        transform.rotation = Quaternion.Euler(0, orientation, angle);
    }

    /// <summary>
    /// Pops the first instance of an object in a list.
    /// </summary>
    /// <typeparam name="T">The type in the list</typeparam>
    /// <param name="list">The list to pop from</param>
    /// <param name="obj">The object to try to pop from the list</param>
    /// <returns>The object requested if present, default if not present</returns>
    public static T Pop<T>(this List<T> list, T obj) {
        T result = default(T);

        int index = list.IndexOf(obj);
        if (index != -1) {
            result = list[index];
            list.RemoveAt(index);
        }

        return result;
    }
}
