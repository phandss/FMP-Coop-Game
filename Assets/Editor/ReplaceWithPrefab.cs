using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefab : EditorWindow
{
    private GameObject _prefab;

    [MenuItem("Tools/Replace With Prefab")]
    public static void Open()
    {
        GetWindow<ReplaceWithPrefab>("Replace With Prefab");
    }

    private void OnGUI()
    {
        _prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace Selected"))
        {
            if (_prefab == null)
            {
                EditorUtility.DisplayDialog("Error", "Assign a prefab first.", "OK");
                return;
            }
            Replace();
        }
    }

    private void Replace()
    {
        GameObject[] selected = Selection.gameObjects;

        Undo.SetCurrentGroupName("Replace With Prefab");
        int group = Undo.GetCurrentGroup();

        foreach (GameObject go in selected)
        {
            GameObject spawned = (GameObject)PrefabUtility.InstantiatePrefab(_prefab, go.transform.parent);
            Undo.RegisterCreatedObjectUndo(spawned, "Replace With Prefab");

            spawned.transform.position = go.transform.position;
            spawned.transform.rotation = go.transform.rotation;
            spawned.transform.localScale = go.transform.localScale;
            spawned.name = go.name;

            Undo.DestroyObjectImmediate(go);
        }

        Undo.CollapseUndoOperations(group);
    }
}
