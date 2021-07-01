using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPlayer : EnkaPlayer
{
    private void Awake()
    {
        playerName = WordRandom.Adjective() + (Random.Range(0f,1f) > .33f ? "" : "_") + WordRandom.Noun();
        name += $" ({PlayerName})";
    }

    public override void StartTurn(GameManager gameManager)
    {
        base.StartTurn(gameManager);

        if (playableCards.Count == 0)
        {
            gameManager.Invoke(nameof(gameManager.NextTurn), 0);
            // gameManager.NextTurn();
            return;
        }
        
        Card _card = playableCards[Random.Range(0, playableCards.Count)];

        if (_card is IColorOption option)
            _card.Color = ChooseColor(option.ColorOptions);

        PlayerCards.Remove(_card);
        gameManager.LastCard = _card;
        
        print($"AI player \"{playerName}\" played a turn, now has {PlayerCards.Count} cards");
    }

    public override void EndTurn()
    {
        // print($"AI player \"{playerName}\" ending turn");
    }

    public override EnkaColor ChooseColor(EnkaColor[] colors) => colors[Random.Range(0, colors.Length)];
}

[CustomEditor(typeof(AIPlayer))]
public class AIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnkaPlayer player = target as EnkaPlayer;

        EditorGUILayout.Space(14f);
        
        int i = 0;
        foreach (Card card in player.PlayerCards)
        {
            i++;
            GUILayout.Label("Card " + i);
            EditorGUILayout.TextField("Type:", card.Type.ToString());
            EditorGUILayout.TextField("Color:", card.Color.ToString());
        }
    }
}