
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolDragDropCE : Editor
{
    private SerializedObject obj;

    public void OnEnable()
    {
        obj = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        ObjectPool op = (ObjectPool)target;

        DrawDefaultInspector();
        EditorGUILayout.Space();
        DropAreaGUI(op);


        if (GUILayout.Button("Reset Pool"))
            op.Clear();
    }

    public void DropAreaGUI(ObjectPool op)
    {
        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag & Drop Prefabs to Pool");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        //op.AddToPrefabsToPool(draggedObject as GameObject);

                    }
                }
                break;
        }
    }
}

/*
[CustomEditor(typeof(Entity))]
public class CE_Entity : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Entity _target = (Entity)target;

        if (GUILayout.Button("Find Graphic"))
            _target.GetGraphic();
    }
}
*/