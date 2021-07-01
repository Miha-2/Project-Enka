using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnkaManager
{
    public static Color[] Colors = {Color.red, Color.blue, Color.yellow, Color.green, Color.black};
    
    public static T[] ShuffleArray<T>(T[] arr) {
        for (int i = arr.Length - 1; i > 0; i--) {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }

        return arr;
    }

    
}

public enum EnkaColor
{
    Red,
    Blue,
    Yellow,
    Green,
    Black
}

public enum EnkaType
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Turn,
    Stop,
    Change,
    Plus2_GY,
    Plus2_RB,
    Plus4,
    Plus5
}

public interface IColorOption
{
    EnkaColor[] ColorOptions { get; }
}

