using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    [HideInInspector] public EnkaPlayer Player;
    [SerializeField] private EnkaType _type;
    public EnkaType Type
    {
        get => _type;
        set => _type = value;
    }


    private EnkaColor _color = EnkaColor.Black;
    public EnkaColor Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                Material.SetColor("Color_Main", EnkaManager.Colors[(int) value]);
            }
            _color = value;
        }
    }


    private Material _material;

    protected Material Material
    {
        get {
            if (!_material) _material = GetComponent<Renderer>().material;

            return _material; 
        }
    }
    
    [ContextMenu("Setup")]
    public virtual void Setup()
    {
        TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro textMesh in texts)
            textMesh.text = ((int) Type).ToString();
    }

    public virtual void OnUse(GameManager manager)
    { 
        
    }

    public virtual bool Validate(Card card)
    {
        return card.Color == Color || card.Type == Type;
    }
    
}

#region Editor
//--------------Temporary for testing--------------//

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Card card = target as Card;

        // card.Type = (EnkaType) card.Number;
    }
}
#endregion
