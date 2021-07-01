using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Turn : Card
{
    protected void Start()
    {
        Type = EnkaType.Turn;
    }

    public override void OnUse(GameManager manager) => manager.IsClockwise = !manager.IsClockwise;
}
