using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class WordRandom
{
    private static string[] nouns = new string[0];
    public static string Noun()
    {
        if (nouns.Length == 0)
        {
            StreamReader reader = new StreamReader("Assets/Name/Nouns.txt");
            nouns = reader.ReadToEnd().Split(' ');
        }

        return nouns[Random.Range(0, nouns.Length)];
    }
    
    private static string[] adjectives = new string[0];
    public static string Adjective()
    {
        if (adjectives.Length == 0)
        {
            StreamReader reader = new StreamReader("Assets/Name/Adjectives.txt");
            adjectives = reader.ReadToEnd().Split(' ');
        }

        return adjectives[Random.Range(0, adjectives.Length)];
    }

    // private static string conjunctors = " -_";
    // public static string Conjunctor()
    // {
    //     return conjunctors[Random.Range(0, conjunctors.Length)].ToString();
    // }
}
