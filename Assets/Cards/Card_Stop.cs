using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card_Stop : Card
{
    protected void Start()
    {
        Type = EnkaType.Stop;
    }

    public override void Setup()
    {
        Material.SetColor("Color_Main", EnkaManager.Colors[(int) Color]);
        
        TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro textMesh in texts)
            textMesh.text = "STOP";
    }

    public override void OnUse(GameManager manager) => manager.IsStopped = true;
}
