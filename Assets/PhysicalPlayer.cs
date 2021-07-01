using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicalPlayer : EnkaPlayer
{
    public bool myTurn = false;
    [HideInInspector] public GameManager CurrentManager;

    public override void StartTurn(GameManager gameManager)
    {
        base.StartTurn(gameManager);
        
        CurrentManager = gameManager;
        print($"Physical player \"{playerName}\" starting turn");
        myTurn = true;
    }
    public override void EndTurn()
    {
        myTurn = false;
    }

    public override EnkaColor ChooseColor(EnkaColor[] colors)
    {
        throw new NotImplementedException();
    }

    public void PlayCard(Card card)
    {
        CurrentManager.LastCard = card;
        playerCards.Remove(card);
        
        //Temporary
        EndTurn();
    }
}


[CustomEditor(typeof(PhysicalPlayer))]
public class PhysicalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PhysicalPlayer player = target as PhysicalPlayer;

        EditorGUILayout.Space(14f);
        
        for (int i = 0; i < player.PlayerCards.Count; i++)
        {
            Card card = player.PlayerCards[i];
            GUILayout.Label("Card " + (i + 1));
            if (player.myTurn && card.Validate(player.CurrentManager.LastCard))
            {
                if (card is IColorOption option)
                {
                    GUILayout.BeginHorizontal();
                    foreach (EnkaColor color in option.ColorOptions)
                    {
                        if (GUILayout.Button("Play as " + color))
                        {
                            card.Color = color;
                            player.PlayCard(card);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                else if (GUILayout.Button("Play"))
                {
                    player.PlayCard(card);
                }
            }

            EditorGUILayout.TextField("Type:", card.Type.ToString());
            EditorGUILayout.TextField("Color:", card.Color.ToString());
            GUILayout.Space(10f);
        }
    }
}