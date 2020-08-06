using System.Collections;
using UnityEditor;
using UnityEngine;

public static class Snapper 
{
    const string UNDO_STR_SNAP = "snap objects";

    [MenuItem("Edit/Snap Selected Objects %&S", isValidateFunction: true)]
    public static bool SnapTheThingsValidate() => Selection.gameObjects.Length > 0;

    [MenuItem("Edit/Snap Selected Objects %&S")]
    public static void SnapTheThings()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, UNDO_STR_SNAP);
            go.transform.position = go.transform.position.Round();
        }
    }

    public static Vector3 Round(this Vector3 v)
    {
        
        v.x = Mathf.Round(v.x/SnapperTool.gridXSize) * SnapperTool.gridXSize;
        v.y = Mathf.Round(v.y/ SnapperTool.gridYSize) * SnapperTool.gridYSize;
        v.z = Mathf.Round(v.z/ SnapperTool.gridZSize) * SnapperTool.gridZSize;
        Debug.Log(v);
        return v;
    }
    static void RoundingTest()
    {

    }
}
