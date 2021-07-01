using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Plus4 : Card, IColorOption
{
    public override void OnUse(GameManager manager)
    {
        base.OnUse(manager);
        manager.Draw(4);
    }
    
    public EnkaColor[] ColorOptions { get; } = {EnkaColor.Red, EnkaColor.Green, EnkaColor.Blue, EnkaColor.Yellow};

    public override bool Validate(Card card) => true;
}
