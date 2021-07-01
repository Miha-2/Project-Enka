using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Change : Card, IColorOption
{
    protected void Start()
    {
        Type = EnkaType.Change;
    }
    
    public EnkaColor[] ColorOptions { get; } = {EnkaColor.Red, EnkaColor.Green, EnkaColor.Blue, EnkaColor.Yellow};
}
