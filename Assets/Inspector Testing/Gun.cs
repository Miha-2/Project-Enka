using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("as")]
    public string gunName = "false";
    public bool canScope = false;
    [HideInInspector] [Range(1,100)] public int scopeScale = 7;
}

[CustomEditor(typeof(Gun))]
public class GunEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        
        var gun = target as Gun;
        
        if (gun.canScope)
            gun.scopeScale = EditorGUILayout.IntSlider(nameof(gun.scopeScale).ToReadable(), gun.scopeScale, 1, 100);
    }
}