using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static System.String;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject card = null;
    [Space]
    [SerializeField] private EnkaPlayer[] players = new EnkaPlayer[0];
    private EnkaPlayer activePlayer;
    private int activeIndex = -1;

    #region Properties

    public EnkaPlayer ActivePlayer => activePlayer;
    public Stack<Card> Pile { get; private set; } = new Stack<Card>();
    private Card _lastCard;
    public Card LastCard
    {
        get => _lastCard;
        set
        {
            _lastCard = value;
            _lastCard.OnUse(this);

            if (ActivePlayer.PlayerCards.Count == 0)
            {
                Debug.LogWarning(ActivePlayer.PlayerName + " WON THE GAME!!!");
                return;
            }
            
            if (!(_lastCard is Card_Plus5))
                // NextTurn();
                Invoke(nameof(NextTurn), 0);
        }
    }

    public bool IsClockwise { get; set; }
    public bool IsStopped { get; set; }

    #endregion

    private void Start()
    {
        players = FindObjectsOfType<EnkaPlayer>();
        
        GeneratePile();

        _lastCard = Pile.Pop();
        if (LastCard is IColorOption option)
            LastCard.Color = option.ColorOptions[Random.Range(0, option.ColorOptions.Length)];

        DistributeCards(6);
        
        NextTurn();
    }

    private void GeneratePile()
    { 
        List<Card> list = new List<Card>();
        //Colored cards
        for (int a = 0; a < 3; a++) {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Card _card = Instantiate(card).AddComponent<Card>();
                    _card.Color = (EnkaColor) i;
                    _card.Type = (EnkaType) j;

                    list.Add(_card);
                }

                for (int j = 0; j < 2; j++)
                {
                    Card _card2 = Instantiate(card).AddComponent<Card_Turn>();
                    _card2.Color = (EnkaColor) i;
                    _card2.Type = EnkaType.Turn;
                    list.Add(_card2);

                    Card _card3 = Instantiate(card).AddComponent<Card_Stop>();
                    _card3.Color = (EnkaColor) i;
                    _card3.Type = EnkaType.Stop;
                    list.Add(_card3);

                    Card _card4 = Instantiate(card).AddComponent<Card_Change>();
                    _card4.Color = (EnkaColor) i;
                    _card4.Type = EnkaType.Change;
                    list.Add(_card4);

                    Card_Plus2 _card5 = Instantiate(card).AddComponent<Card_Plus2>();
                    bool ver = j == 0;
                    _card5.ColorOptions = new[]
                        {ver ? EnkaColor.Red : EnkaColor.Green, ver ? EnkaColor.Blue : EnkaColor.Yellow};
                    _card5.Color = EnkaColor.Black;
                    _card5.Type = ver ? EnkaType.Plus2_RB : EnkaType.Plus2_GY;
                    list.Add(_card5);
                }

                Card _card6 = Instantiate(card).AddComponent<Card_Plus4>();
                _card6.Color = EnkaColor.Black;
                _card6.Type = EnkaType.Plus4;
                list.Add(_card6);
            }
        }
        // Card _card7 = Instantiate(card).AddComponent<Card_Plus5>();
        // _card7.Color = EnkaColor.Black;
        // _card7.Type = EnkaType.Plus5;
        // list.Add(_card7);

        foreach (Card card1 in list)
            card1.Setup();

        //Shuffle
        Pile = new Stack<Card>(EnkaManager.ShuffleArray(list.ToArray()));
    }

    private void DistributeCards(int amount)
    {
        if (amount < 1)
        {
            Debug.LogError($"Inputed amount ({amount}), was smaller than required (1)");
            return;
        }

        foreach (EnkaPlayer player in players)
        {
            for (int i = 0; i < amount; i++)
            {
                GetCard(player);
            }
        }
    }
    
    public void NextTurn()
    {
        if (ActivePlayer == null) activeIndex = Random.Range(0, players.Length);
        else
        {
            ActivePlayer.EndTurn();
            int index = IsClockwise ? 1 : -1;
            index *= IsStopped ? 2 : 1;
            activeIndex = (activeIndex + index + players.Length) % players.Length;
        }

        IsStopped = false;
        activePlayer = players[activeIndex];

        if (drawQueue > 0)
        {
            for (int i = 0; i < drawQueue; i++)
            {
                GetCard(activePlayer);
            }
            drawQueue = 0;
            // NextTurn();
            Invoke(nameof(NextTurn), 0);
        }
        else
            ActivePlayer.StartTurn(this);

    }

    private int drawQueue = 0;

    public void Draw(int amount)
    {
        drawQueue += amount;
    }
    public Card GetCard(EnkaPlayer player)
    {
        Card _card = Pile.Pop();
        _card.Player = player;
        player.PlayerCards.Add(_card);
        return _card;
    }

    public void Draw5()
    {
        foreach (EnkaPlayer player in players)
        {
            if (player == activePlayer)
                continue;

            for (int i = 0; i < 5; i++)
                GetCard(player);
        }
    }
}

#region EditorInfo
[CustomEditor(typeof(GameManager))]
public class ManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager manager = target as GameManager;

        // ReSharper disable once PossibleNullReferenceException
        EditorGUILayout.Space(14f);
        if (GUILayout.Button("New turn")) manager.NextTurn();

        string activeName = manager.ActivePlayer == null ? Empty : manager.ActivePlayer.PlayerName;
        EditorGUILayout.Space(14f);
        GUILayout.Label("Active player:   " + activeName, EditorStyles.boldLabel);
        EditorGUILayout.Space(14f);

        GUILayout.Label("Active card:", EditorStyles.boldLabel);
        if (manager.LastCard != null)
        {
            GUILayout.Label("Active type:   " + manager.LastCard.Type);
            GUILayout.Label("Active color:   " + manager.LastCard.Color);
        }
        
        EditorGUILayout.Space(14f);

        int i = 0;
        foreach (Card card in manager.Pile)
        {
            i++;
            GUILayout.Label("Card " + i);
            EditorGUILayout.TextField("Type:", card.Type.ToString());
            EditorGUILayout.TextField("Color:", card.Color.ToString());
        }
        
    }
}
#endregion