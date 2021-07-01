using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Testing : EditorWindow
{
    private string abc = "";

    [MenuItem("Tools/Testing Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(Testing));
    }
    
    private void OnGUI()
    {
        GUILayout.Label("This is a label", EditorStyles.colorField);
        
        abc = EditorGUILayout.TextField("This is a label", abc);
        EditorGUILayout.PrefixLabel("Prefixed label");

        if (GUILayout.Button("Add random number"))
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                obj.name += Random.Range(0, 10);
            }
        }
        if (GUILayout.Button("Remove number"))
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (!int.TryParse(obj.name[obj.name.Length - 1].ToString(), out int a))
                    continue;
                obj.name = obj.name.Substring(0, obj.name.Length - 1);
            }
        }
    }
}
