
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Utilities
{
    public static T[] GetItems<T>(Transform parent)
    {
        if (parent != null && parent.childCount > 0)
        {
            // list to store all detected components
            List<T> componentList = new List<T>();

            // loop to find components
            for (int i = 0; i < parent.childCount; i++)
            {
                var component = parent.GetChild(i).GetComponent<T>();
                if (component != null && componentList.Contains(component) == false)
                    componentList.Add(component);
            }

            // return array of found components
            return componentList.ToArray();
        }

        else
        {
            Debug.Log("Parent has no children");
            return null;
        }
    }

    public static void DestroyChildrentOfTranform(Transform parent)
    {
        while (parent.childCount > 0)
            for (int i = 0; i < parent.childCount; i++)
            {
                if (Application.isEditor)
                    GameObject.DestroyImmediate(parent.GetChild(i));
                else
                    GameObject.Destroy(parent.GetChild(i));
            }

        if (parent.childCount == 0)
            Debug.Log("Children Destroyed!");
        else
            Debug.Log("Children not fully destroyed...");
    }

    public static Transform CreateEmpty(string name, Vector3 position, Transform parent)
    {
        Transform point = new GameObject(name).transform;
        point.position = position;
        point.parent = parent;
        return point;
    }

    public static List<T> AddArrayToList<T>(this List<T> listToAdd, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            listToAdd.AddSafe(array[i]);

        return listToAdd;
    }

    public static T[] ReverseArray<T>(T[] array)
    {
        var reversed = new T[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int index = Mathf.Clamp(array.Length - 1 - i, 0, array.Length);
            reversed[index] = array[i];
        }

        return reversed;
    }


#if UNITY_EDITOR
    public static void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #region Gizmos

    public static void DrawCube(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }

    public static void DrawString(string text, Vector3 worldPos, Color? color = null)
    {
        Handles.BeginGUI();
        if (color.HasValue) GUI.color = color.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        Handles.EndGUI();
    }
    #endregion // gizmos

    #region Instantiate Prefabs
#if UNITY_EDITOR
    public static GameObject CreatePrefab(GameObject prefab)
        => PrefabUtility.InstantiatePrefab(prefab) as GameObject;

    public static GameObject CreatePrefab(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj;
    }

    public static T CreatePrefab<T>(GameObject prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    public static T CreatePrefab<T>(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj.GetComponent<T>();
    }
#endif
    #endregion // instantiate prefabs
#endif
}