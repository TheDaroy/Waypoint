using System.Collections;
using UnityEditor;
using UnityEngine;

public class SnapperTool : EditorWindow
{
    [MenuItem("Tools/Snapper")]
    public static void OpenTheThing() => GetWindow<SnapperTool>("Snapper");


    static public float gridXSize ;
    static public float gridYSize ;
    static public float gridZSize ;

    static public int mapGridXAmmount ;
    static public int mapGridYAmmount ;
    static public int mapGridZAmmount ;

    void OnEnable()
    {
        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    void DuringSceneGUI(SceneView sceneView)
    {
        DrawMapGrid();
    }

    void OnGUI()
    {
        using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Snap Selection"))
                SnapSelection();
        }
        gridXSize = EditorGUILayout.FloatField("x", gridXSize = Mathf.Clamp(gridXSize, 0.0001f, float.MaxValue));
        gridYSize = EditorGUILayout.FloatField("y", gridYSize = Mathf.Clamp(gridYSize, 0.0001f, float.MaxValue));
        gridZSize = EditorGUILayout.FloatField("z", gridZSize = Mathf.Clamp(gridZSize, 0.0001f, float.MaxValue));

        mapGridXAmmount = EditorGUILayout.IntField("Number Of X Grids", mapGridXAmmount = Mathf.Clamp(mapGridXAmmount, 0, int.MaxValue));
        mapGridYAmmount = EditorGUILayout.IntField("Number Of Y Grids", mapGridYAmmount = Mathf.Clamp(mapGridYAmmount, 0, int.MaxValue));
        mapGridZAmmount = EditorGUILayout.IntField("Number Of Z Grids", mapGridZAmmount = Mathf.Clamp(mapGridZAmmount, 0, int.MaxValue));
    }
    void DrawMapGrid()
    {
        Vector3 temp = new Vector3(0, 0, 0);
        if (mapGridYAmmount >0)
        {
            for (int y = 0; y < mapGridYAmmount+1; y++)
            {
                for (int x = 0; x < mapGridXAmmount + 1; x++)
                {
                    for (int z = 0; z < mapGridZAmmount + 1; z++)
                    {
                        Handles.DrawLine(new Vector3(gridXSize * x, 0, gridZSize * z), new Vector3(gridXSize * x, gridZSize * mapGridYAmmount, gridZSize * z));

                        Handles.DrawLine(new Vector3(gridXSize * x, gridYSize * y, 0), new Vector3(gridXSize * x, gridYSize * y, gridZSize * mapGridZAmmount));
                        Handles.DrawLine(new Vector3(0, gridYSize * y, gridZSize * z), new Vector3(gridXSize * mapGridXAmmount, gridYSize * y, gridZSize * z));
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < mapGridXAmmount + 1; x++)
            {
                for (int z = 0; z < mapGridZAmmount + 1; z++)
                {
                   

                    Handles.DrawLine(new Vector3(gridXSize * x, 0, 0), new Vector3(gridXSize * x, 0, gridZSize * mapGridZAmmount));
                    Handles.DrawLine(new Vector3(0, 0, gridZSize * z), new Vector3(gridXSize * mapGridXAmmount, 0, gridZSize * z));
                }
            }
        }
        
        
        
    }
    void SnapSelection()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, "snap objects");
            go.transform.position = go.transform.position.Round();
        }
    }

}

