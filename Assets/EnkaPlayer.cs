using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnkaPlayer : MonoBehaviour
{
    [SerializeField] protected string playerName;
    protected List<Card> playerCards = new List<Card>();

    protected List<Card> playableCards;

    public virtual void StartTurn(GameManager gameManager)
    {
        playableCards = playerCards.Where(card => card.Validate(gameManager.LastCard)).ToList();
        if (playableCards.Count == 0)
        {
            Card newCard = gameManager.GetCard(this);
            if (newCard.Validate(gameManager.LastCard)) playableCards.Add(newCard);
        }
    }
    
    public virtual void EndTurn(){}

    public string PlayerName => playerName;

    public List<Card> PlayerCards => playerCards;

    public virtual EnkaColor ChooseColor(EnkaColor[] colors) => throw new NotImplementedException();
}