using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Plus2 : Card, IColorOption
{
    public EnkaColor[] ColorOptions { get; set; }
    
    public override void OnUse(GameManager manager)
    {
        base.OnUse(manager);
        manager.Draw(2);
    }
    
    public override bool Validate(Card card)
    {
        return card.Color == ColorOptions[0] || card.Color == ColorOptions[1];
    }
}
